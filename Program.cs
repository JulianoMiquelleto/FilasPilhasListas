

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AppListasFilasPilha
{
    public struct Candidato
    {
        public string Nome;
        public decimal NotaFinal;
        public int[] OpcaoCurso;

        public Candidato(string nome, decimal nota, int[] opcoes)
        {
            Nome = nome;
            NotaFinal = nota;
            OpcaoCurso = opcoes;
        }
    }

    public struct Curso
    {
        public int Id;
        public String Nome;
        public Pilha<Candidato> Candidatos;

        public Curso(int id, int nunVagas,string nome)
        {
            Id = id;
            Candidatos = new Pilha<Candidato>(nunVagas);
            Nome = nome;

        }

         public static void OrdenaNotas(Candidato[] lista, string direction) {
          Candidato aux;
          for (int i = 0; i < lista.Length; i++)
          {
              for (int j = i + 1; j < lista.Length; j++)
              {
                  bool change = false;
                  if (direction == "DESC") {
                      if (lista[i].NotaFinal < lista[j].NotaFinal)
                        change = true;
                  } else
                    if (lista[i].NotaFinal > lista[j].NotaFinal)
                      change = true;

                  if (change) {
                    aux = lista[i];
                      lista[i] = lista[j];
                      lista[j] = aux;
                  }
              }
          }
        }

    }

    class Program
    {
        //static Candidato[] lerCsv()
        //{
        //TODO::lerCsv

        //cria lista com capacidade de 10 canditados
        //Candidato[] lista = new Candidato[5];

        //  //carrega candidatos
        //  lista[0] = new Candidato("1", 9,  new int[] { 1,2,3 }));
        //  lista[1] = new Candidato("2", 7,  new int[] { 1, 3, 4 }));
        //  lista[2] = new Candidato("3", 5,  new int[] { 4, 1, 2 }));
        //  lista[3] = new Candidato("4", 10, new int[] { 4, 1, 2 }));
        //  lista[4] = new Candidato("5", 9,  new int[] { 4, 5, 3 }));
        //}

        static void Main(string[] args)
        {
            int numVagas = 2;

            //Criar os cursos
            Curso[] cursos = new Curso[7];
            cursos[0] = new Curso(1, numVagas,"Eng da computação");
            cursos[1] = new Curso(2, numVagas, "Medicina");
            cursos[2] = new Curso(3, numVagas, "Analise de sistemas");
            cursos[3] = new Curso(4, numVagas, "Farmacia");
            cursos[4] = new Curso(5, numVagas, "Administração");
            cursos[5] = new Curso(6, numVagas, "Direito");
            cursos[6] = new Curso(7, numVagas, "Nutrição");

            //Carrega os candidatos
            Candidato[] candidatos = lerCsv();

            //Ordena candidatos pela maior nota e adiciona na fila para inscrições nos cursos
            Curso.OrdenaNotas(candidatos, "DESC");
            Fila<Candidato> incricoes = new Fila<Candidato>(candidatos);

            //Enquanto possuir inscrições procura pelas vagas
            while (!incricoes.empty())
            {
                try
                {
                    Candidato candidato = incricoes.Desenfileirar();
                    //Percorre as opções de cursos
                    foreach (int opcaoCurso in candidato.OpcaoCurso)
                    {
                        //Para não gerar exceção verifica se existe o curso
                        if (opcaoCurso > 7) // codigo dos cursos
                        {
                            Console.WriteLine("Opção de curso " + opcaoCurso + " para o candidato " + candidato.Nome +
                                              " inexistente.");
                            continue;
                        }

                        //Tenta empilhar o candidato no curso desejado, se não der estouro de pilha a vaga existe, do contrario tenta outro curso
                        try
                        {
                            for (int i = 0; i < cursos.Length; i++)
                            {
                                if (cursos[i].Id == opcaoCurso)
                                {
                                    cursos[i].Candidatos.Empilhar(candidato);
                                }
                            }
                         
                            Console.WriteLine("Candidato " + candidato.Nome + " adicionado ao curso " + opcaoCurso);
                        }
                        catch (StackOverflowException err)
                        {
                            Console.WriteLine("Curso " + opcaoCurso +
                                              " lotado. Verificando outra vaga para o candidato " + candidato.Nome);
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // todos os candidatos foram desifileirados
                }

            }

            //Mostra os Canditados nos cursos
            for (int j = 0; j < cursos.Length; j++)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Candidatos e suas notas inscritos no curso " + cursos[j].Id +"-"+cursos[j].Nome);
                Console.WriteLine("--------------------------------");
              
                while (!cursos[j].Candidatos.Empty())
                {
                    Candidato candidato = cursos[j].Candidatos.Desempilhar();
                    Console.Write("candidato:" + candidato.Nome + " nota:" + candidato.NotaFinal + " , ");
                }
                Console.WriteLine("--------------------------------");
            }

            Console.ReadKey();

        }

        private static Candidato[] lerCsv()
        {
            int j = 0;
            int countRegistersCsv = CountRegisters(@"c:\Temp\inscricoes.csv");
            Candidato[] lista = new Candidato[countRegistersCsv];
            using (var reader = new StreamReader(@"c:\Temp\inscricoes.csv"))
            {           
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (j > 0)
                    {
                        var values = line.Split(';');
                        int[] opcoes = new int[3];
                        string[] cursos = values[2].Split(',');
                        for (int i = 0; i < cursos.Length; i++)
                        {
                            opcoes[i] = Convert.ToInt32(cursos[i]);
                        }
                        lista[j-1] = new Candidato(values[0].ToString(), Convert.ToDecimal( values[1]), opcoes);
                    }
                    j++;
                }
            }

            return lista;


        }

        private static int CountRegisters(string file)
        {
            int i = 0;
            using (var reader = new StreamReader(file))
            {
                //conta os registros, o primeiro registro é o header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    i++;
                }
            }
            //primeiro registro é o header
            return i-1;
        }

    }
   


    //Fila
    public class Fila<T>
    {
        readonly int tamanho;
        int inicio = 0;
        int fim = -1;
        int tamanhoFila = 0;
        T[] itens;

        public Fila(int size)
        {
            tamanho = size;
            itens = new T[tamanho];
        }

        public Fila(T[] pitens)
        {
            fim =
                tamanhoFila =
                    tamanho = pitens.Length;
            itens = pitens;
        }

        public Boolean empty()
        {
            return tamanhoFila == 0;
        }

        public void Enfileirar(T item)
        {
            if ((tamanhoFila + 1) > tamanho) throw new StackOverflowException();

            int fim = 0;
            fim = (fim + 1 % tamanho);
            itens[fim] = item;
            tamanhoFila++;
        }

        public T Desenfileirar()
        {
            if (tamanhoFila - 1 < 0)
            {
                throw new InvalidOperationException("A pilha esta vazia");
            }
            try
            {
                tamanhoFila--;
                return itens[inicio];
            }
            finally
            {
                inicio = (inicio + 1 % tamanho);
            }
        }
    }

    //Pilha
	public class Pilha<T>
	{
	   readonly int _tamanho;
	   int posicao = 0;
	   T[] itens;

	   public Pilha(int size)
	   {
		  _tamanho = size;
		  itens = new T[_tamanho];
	   }
     public Boolean Empty()
     {
       return posicao == 0;
     }
	   public void Empilhar(T item)
	   {
		  if(posicao >= _tamanho)  throw new StackOverflowException();

		  itens[posicao] = item;
		  posicao++;
	   }
	   public T Desempilhar()
	   {
		  posicao--;
		  if(posicao >= 0)
		  {
		     return itens[posicao];
		  }
		  else
		  {
		     posicao = 0;
		     throw new InvalidOperationException("A pilha esta vazia");
		  }
	   }
	}
}
