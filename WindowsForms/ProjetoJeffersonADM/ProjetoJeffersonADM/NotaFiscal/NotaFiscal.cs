using Estoque;
using Pedidos;
using ProdutoDLL;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Usuario;

namespace ProjetoJeffersonADM 
{ 
    public class NotaFiscal
    {
    public void EmitirNotaFiscal(string nomeSemEspacos,string nomeCli,string cpf,string rua,string cidade,string telefone,int idProduto,string nomeProduto, int quantidade, double precoUnitario,double valorTotal,string formaPagamento) {

            string diretorioAtual = Directory.GetCurrentDirectory();
            string pastaNotaFiscal = Path.Combine(diretorioAtual, @"NotaFiscal\Pdfs");
            if (!Directory.Exists(pastaNotaFiscal))
            {
                Directory.CreateDirectory(pastaNotaFiscal);
            }
            string caminhoFiscal = Path.Combine(pastaNotaFiscal, $"{nomeSemEspacos}.pdf");
            string logoPath = Path.Combine(diretorioAtual, @"NotaFiscal\Logo\receita_federal.png");

            double icms = valorTotal * 0.18;
            double ipi = valorTotal * 0.10;

            DateTime data = DateTime.Now;


            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Row(row =>
                    {
                     row.ConstantItem(100).Height(50).Image(logoPath);
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("NOTA FISCAL").FontSize(20).Bold().AlignCenter();
                            col.Item().AlignCenter().Text("Dribe Modas").FontSize(16).Bold().AlignCenter();
                        });
                    });

                    page.Content().Column(column =>
                    {
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("EMITENTE").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("NOME / RAZÃO SOCIAL:").Bold();
                            table.Cell().Text("Dribe Modas");
                            table.Cell().Text("ENDEREÇO:").Bold();
                            table.Cell().Text("Av. Alda 123");
                            table.Cell().Text("MUNICÍPIO / UF:").Bold();
                            table.Cell().Text("Diadema / EX");
                            table.Cell().Text("FONE / FAX:").Bold();
                            table.Cell().Text("11939282007");
                            table.Cell().Text("CNPJ / INSCRIÇÃO ESTADUAL:").Bold();
                            table.Cell().Text("93.856.732/0001-69");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("DESTINATÁRIO / REMETENTE").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("NOME / RAZÃO SOCIAL:").Bold();
                            table.Cell().Text($"{nomeCli}");
                            table.Cell().Text("CNPJ / CPF:").Bold();
                            table.Cell().Text($"{cpf}");
                            table.Cell().Text("ENDEREÇO:").Bold();
                            table.Cell().Text($"{rua}");
                            table.Cell().Text("MUNICÍPIO / UF:").Bold();
                            table.Cell().Text($"{cidade} / CL");
                            table.Cell().Text("FONE / FAX:").Bold();
                            table.Cell().Text($"{telefone}");
                            table.Cell().Text("INSCRIÇÃO ESTADUAL:").Bold();
                            table.Cell().Text("9876543210");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("INFORMAÇÕES DO PEDIDO").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("Data do Pedido:").Bold();
                            table.Cell().Text($"{data:dd/MM/yyyy}");
                            table.Cell().Text("ID do Produto:").Bold();
                            table.Cell().Text($"{idProduto}");
                            table.Cell().Text("Nome do Produto:").Bold();
                            table.Cell().Text($"{nomeProduto}");
                            table.Cell().Text("Fabricante:").Bold();
                            table.Cell().Text($"Nike");
                            table.Cell().Text("Quantidade:").Bold();
                            table.Cell().Text($"{quantidade}");
                            table.Cell().Text("Preço Unitário:").Bold();
                            table.Cell().Text($"{precoUnitario:C}");
                            table.Cell().Text("Valor Total:").Bold();
                            table.Cell().Text($"{valorTotal:C}");
                            table.Cell().Text("Forma de Pagamento:").Bold();
                            table.Cell().Text($"{formaPagamento}");
                            table.Cell().Text("Tipo de Nota:").Bold();
                            table.Cell().Text($"Saida");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("TRIBUTOS").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("ICMS:").Bold();
                            table.Cell().Text($"{icms:C}");
                            table.Cell().Text("IPI:").Bold();
                            table.Cell().Text($"{ipi:C}");
                        });
                    });

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(40).AlignCenter().Text("_____________________________");
                            col.Item().AlignCenter().Text("Assinatura do Emitente");
                            col.Item().PaddingTop(10).AlignCenter().Text("Dribe Modas").Italic();
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(40).AlignCenter().Text("_____________________________");
                            col.Item().AlignCenter().Text("Assinatura do Destinatário");
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(20).AlignRight().Text("Documento gerado eletronicamente");
                            col.Item().AlignRight().Text("https://dribemodas.com.br").Italic();
                        });
                    });
                });


            }).GeneratePdf(caminhoFiscal);

            MessageBox.Show("PDF criado com sucesso!");



        }


        public void EmitirNotaFiscalEstoque(string nomeSemEspacos,string nomeFornecedor , string cnpj, string rua, string cidade,string estado, string telefone, int idProduto, string nomeProduto, int quantidade, double precoUnitario, double valorTotal, string formaPagamento)
        {
            string baseDiretorio = AppDomain.CurrentDomain.BaseDirectory;
            string pastaRelativa = @"NotaFiscal\Pdfs";
            string pastaNotaFiscal = Path.Combine(baseDiretorio, pastaRelativa);

            if (!Directory.Exists(pastaNotaFiscal))
            {
                Directory.CreateDirectory(pastaNotaFiscal);
            }

            string caminhoFiscal = Path.Combine(pastaNotaFiscal, $"{nomeSemEspacos}.pdf");
            string logoPath = Path.Combine(baseDiretorio, @"NotaFiscal\Logo\receita_federal.png");

            double icms = valorTotal * 0.18;
            double ipi = valorTotal * 0.10;

            DateTime data = DateTime.Today;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Row(row =>
                    {
                        row.ConstantItem(100).Height(50).Image(logoPath);
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("NOTA FISCAL").FontSize(20).Bold().AlignCenter();
                            col.Item().AlignCenter().Text("Dribe Modas").FontSize(16).Bold().AlignCenter();
                        });
                    });

                    page.Content().Column(column =>
                    {
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("LOJA").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("NOME / RAZÃO SOCIAL:").Bold();
                            table.Cell().Text("Dribe Modas");
                            table.Cell().Text("ENDEREÇO:").Bold();
                            table.Cell().Text("Av. Alda 123");
                            table.Cell().Text("MUNICÍPIO / UF:").Bold();
                            table.Cell().Text("Diadema / EX");
                            table.Cell().Text("FONE / FAX:").Bold();
                            table.Cell().Text("11939282007");
                            table.Cell().Text("CNPJ / INSCRIÇÃO ESTADUAL:").Bold();
                            table.Cell().Text("93.856.732/0001-69");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("FABRICANTE").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("NOME / RAZÃO SOCIAL:").Bold();
                            table.Cell().Text($"{nomeFornecedor}");
                            table.Cell().Text("CNPJ / CPF:").Bold();
                            table.Cell().Text($"{cnpj}");
                            table.Cell().Text("ENDEREÇO:").Bold();
                            table.Cell().Text($"{rua}");
                            table.Cell().Text("MUNICÍPIO / UF:").Bold();
                            table.Cell().Text($"{cidade} / {estado}");
                            table.Cell().Text("INSCRIÇÃO ESTADUAL:").Bold();
                            table.Cell().Text($"324655243534-00");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("INFORMAÇÕES DO PEDIDO").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("Data do Pedido:").Bold();
                            table.Cell().Text($"{data:dd/MM/yyyy}");
                            table.Cell().Text("ID do Produto:").Bold();
                            table.Cell().Text($"{idProduto}");
                            table.Cell().Text("Nome do Produto:").Bold();
                            table.Cell().Text($"{nomeProduto}");
                            table.Cell().Text("Fabricante:").Bold();
                            table.Cell().Text($"{nomeFornecedor}");
                            table.Cell().Text("Quantidade:").Bold();
                            table.Cell().Text($"{quantidade}");
                            table.Cell().Text("Preço Unitário:").Bold();
                            table.Cell().Text($"{precoUnitario:C}");
                            table.Cell().Text("Valor Total:").Bold();
                            table.Cell().Text($"{valorTotal:C}");
                            table.Cell().Text("Forma de Pagamento:").Bold();
                            table.Cell().Text($"{formaPagamento}");
                            table.Cell().Text("Tipo de Nota:").Bold();
                            table.Cell().Text($"Entrada");
                        });

                        column.Item().PaddingVertical(15).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("TRIBUTOS").FontSize(16).Bold().Underline();
                                header.Cell().PaddingBottom(10);
                            });

                            table.Cell().Text("ICMS:").Bold();
                            table.Cell().Text($"{icms:C}");
                            table.Cell().Text("IPI:").Bold();
                            table.Cell().Text($"{ipi:C}");
                        });
                    });

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(40).AlignCenter().Text("_____________________________");
                            col.Item().AlignCenter().Text("Assinatura do Emitente");
                            col.Item().PaddingTop(10).AlignCenter().Text("Dribe Modas").Italic();
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(40).AlignCenter().Text("_____________________________");
                            col.Item().AlignCenter().Text("Assinatura do Destinatário");
                            col.Item().PaddingTop(10).AlignCenter().Text($"{nomeFornecedor}").Italic();

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().PaddingTop(20).AlignRight().Text("Documento gerado eletronicamente");
                            col.Item().AlignRight().Text("https://dribemodas.com.br").Italic();
                        });
                    });
                });
            }).GeneratePdf(caminhoFiscal);


            MessageBox.Show("PDF criado com sucesso!");



        }
    }
}
