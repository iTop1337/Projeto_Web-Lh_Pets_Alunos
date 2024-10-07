using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Projeto_Web_Lh_Pets_versao_1
{
    class Banco
    {
        private List<Clientes> lista=new List<Clientes>();

        public List<Clientes> GetLista()
        {
            return lista;
        }

        public Banco()
        {
            try
            {
                // String de conexão diretamente usada
                string connectionString = "User ID=sa;Password=12345;" +
                                          "Server=localhost\\SQLEXPRESS;" +
                                          "Database=vendas;" +
                                          "Trusted_Connection=False;";

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    String sql = "SELECT * FROM tblclientes";
                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        conexao.Open();  // Abrir a conexão
                        using (SqlDataReader tabela = comando.ExecuteReader())  // Executar e ler os dados
                        {
                            while (tabela.Read())
                            {
                                lista.Add(new Clientes()
                                {
                                    cpf_cnpj = tabela["cpf_cnpj"].ToString(),
                                    nome = tabela["nome"].ToString(),
                                    endereco = tabela["endereco"].ToString(),
                                    rg_ie = tabela["rg_ie"].ToString(),
                                    tipo = tabela["tipo"].ToString(),
                                    valor = (float)Convert.ToDecimal(tabela["valor"]),
                                    valor_imposto = (float)Convert.ToDecimal(tabela["valor_imposto"]),
                                    total = (float)Convert.ToDecimal(tabela["total"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException e)  // Para erros de SQL
            {
                Console.WriteLine($"Erro SQL: {e.Message}");
            }
            catch (Exception e)  // Para outros erros gerais
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }

        public String GetListaString()
        {
            string enviar = "<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8' />\n" +
                            "<title>Cadastro de Clientes</title>\n</head>\n<body>";
            enviar = enviar + "<b>   CPF / CNPJ    -      Nome    -    Endereço    -   RG / IE   -   Tipo  -   Valor   - Valor Imposto -   Total  </b>";

            int i = 0;
            string corfundo = "", cortexto = "";

            foreach (Clientes cli in lista)
            {
                if (i % 2 == 0)
                {
                    corfundo = "#6f47ff";
                    cortexto = "white";
                }
                else
                {
                    corfundo = "#ffffff";
                    cortexto = "#6f47ff";
                }
                i++;

                enviar = enviar +
                         $"\n<br><div style='background-color:{corfundo};color:{cortexto};'>" +
                         cli.cpf_cnpj + " - " +
                         cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                         cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                         cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C") + "<br>" +
                         "</div>";
            }
            return enviar;
        }

        public void imprimirListaConsole()
        {
            Console.WriteLine("   CPF / CNPJ   " + " - " + "    Nome   " +
                              " - " + "   Endereço   " + " - " + "  RG / IE  " + " - " +
                              "  Tipo " + " - " + "  Valor  " + " - " + "Valor Imposto" +
                              " - " + "  Total  ");

            foreach (Clientes cli in lista)
            {
                Console.WriteLine(cli.cpf_cnpj + " - " +
                                  cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                                  cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                                  cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C"));
            }
        }
    }

    public class Clientes
    {
        public string cpf_cnpj { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string rg_ie { get; set; }
        public string tipo { get; set; }
        public float valor { get; set; }
        public float valor_imposto { get; set; }
        public float total { get; set; }
    }
}
