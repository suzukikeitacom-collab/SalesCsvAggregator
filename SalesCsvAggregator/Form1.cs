using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesCsvAggregator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // DataGridView の基本設定
            dgvResult.AutoGenerateColumns = true;
            dgvResult.AllowUserToAddRows = false;
            dgvResult.ReadOnly = true;
            dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResult.RowHeadersVisible = false;
        }

        // 「CSVを選択」ボタン
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";
                ofd.Title = "売上CSVファイルを選択してください";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtInputPath.Text = ofd.FileName;
                    lblStatus.Text = "ファイルが選択されました。";
                }
            }
        }

        // 「集計開始」ボタン
        private void btnAggregate_Click(object sender, EventArgs e)
        {
            var path = txtInputPath.Text;

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                MessageBox.Show("先にCSVファイルを選択してください。", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 得意先ごとの合計金額を集計する Dictionary
                var customerTotals = new Dictionary<string, decimal>();

                // CSVを読み込み（文字コードは環境に合わせて変更可：SJIS/UTF8など）
                var lines = File.ReadAllLines(path, Encoding.UTF8);

                if (lines.Length <= 1)
                {
                    MessageBox.Show("データ行がありません。", "エラー",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1行目はヘッダーとして扱う前提
                // 例：日付,品目,個数,単価,得意先
                for (int i = 1; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var cols = line.Split(',');

                    if (cols.Length < 5)
                    {
                        // 想定より列が少ない行はスキップ
                        continue;
                    }

                    // 列の想定：0:日付, 1:品目, 2:個数, 3:単価, 4:得意先
                    string customer = cols[4].Trim();

                    // 個数や単価のパース
                    if (!int.TryParse(cols[2], out int quantity))
                        continue;

                    if (!decimal.TryParse(cols[3], out decimal unitPrice))
                        continue;

                    decimal amount = quantity * unitPrice;

                    if (customerTotals.ContainsKey(customer))
                    {
                        customerTotals[customer] += amount;
                    }
                    else
                    {
                        customerTotals[customer] = amount;
                    }
                }

                if (customerTotals.Count == 0)
                {
                    MessageBox.Show("有効なデータがありませんでした。", "結果",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 得意先ごとの集計結果を画面にも表示
                var gridSource = customerTotals
                    .OrderBy(k => k.Key)
                    .Select(k => new
                    {
                        得意先 = k.Key,
                        合計金額 = k.Value
                    })
                    .ToList();

                dgvResult.DataSource = gridSource;

                // ここで列の表示形式を整える
                if (dgvResult.Columns["合計金額"] != null)
                {
                    dgvResult.Columns["合計金額"].DefaultCellStyle.Format = "N0"; // 3桁区切り
                    dgvResult.Columns["合計金額"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }

                // 得意先の列は広めに
                if (dgvResult.Columns["得意先"] != null)
                {
                    dgvResult.Columns["得意先"].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }

                // 出力先をユーザーに選んでもらう
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSVファイル (*.csv)|*.csv";
                    sfd.Title = "集計結果の保存先を選択してください";
                    sfd.FileName = "得意先別集計_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var outPath = sfd.FileName;

                        var outputLines = new List<string>();
                        // ヘッダー
                        outputLines.Add("得意先,合計金額");

                        // 得意先名でソートして出力
                        foreach (var kv in customerTotals.OrderBy(k => k.Key))
                        {
                            var line = $"{kv.Key},{kv.Value}";
                            outputLines.Add(line);
                        }

                        File.WriteAllLines(outPath, outputLines, Encoding.UTF8);

                        var totalAmount = customerTotals.Values.Sum();
                        lblStatus.Text =
                            $"得意先 {customerTotals.Count} 件、合計金額 {totalAmount:N0} 円を集計しました。";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。\n\n" + ex.Message, "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
