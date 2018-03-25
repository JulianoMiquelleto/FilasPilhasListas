

using System;
using System.Collections;
using System.Globalization;

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

        public Curso(int id, int nunVagas)
        {
            Id = id;
            Candidatos = new Pilha<Candidato>(Vagas);
        }

        static public void OrdenaNotas(Candidato[] lista, string direction) {
          Candidato aux;
          for (int i = 0; i < lista.Length; i++)
          {
              for (int j = i + 1; j < lista.Length; j++)
              {
                  bool change = false;
                  if (direction == "DESC") {
                      if (list[i].NotaFinal < list[j].NotaFinal)
                        change = true;
                  } else
                    if (list[i].NotaFinal > list[j].NotaFinal)
                      change = true;

                  if (change) {
                    aux = list[i];
                    list[i] = list[j];
                    list[j] = aux;
                  }
              }
          }
        }
    }

    class Program
    {
        static Candidato[] lerCsv() {
          //TODO::lerCsv

          //cria lista com capacidade de 10 canditados
          Candidato[] lista = new Candidato[5];

          //carrega candidatos
          lista[0] = new Candidato("1", 9,  new int[] { 1,2,3 }));
          lista[1] = new Candidato("2", 7,  new int[] { 1, 3, 4 }));
          lista[2] = new Candidato("3", 5,  new int[] { 4, 1, 2 }));
          lista[3] = new Candidato("4", 10, new int[] { 4, 1, 2 }));
          lista[4] = new Candidato("5", 9,  new int[] { 4, 5, 3 }));
        }

        static void Main(string[] args)
        {
            int numVagas = 2;

            //Criar os cursos
            Curso[] cursos = new Curso[7];
            for (int j = 0; j < cursos.Length; j++)
            {
              cursos = new Cursos(j, numVagas);
            }

            //Carrega os candidatos
            Candidato[] candidatos = lerCsv();

            //Ordena candidatos pela maior nota e adiciona na fila para inscrições nos cursos
            Curso.OrdenaNotas(candidatos,"DESC");
            Fila<Candidato> incricoes = new Fila<Candidato>( candidatos );

            //Enquanto possuir inscrições procura pelas vagas
            while(!incricoes.empty())
            {
                Candidato candidato = inscricoes.Desenfileirar();
                //Percorre as opções de cursos
                foreach(int opcaoCurso in candidato.OpcaoCurso)
                {
                  //Para não gerar exceção verifica se existe o curso
                  if (cursos.Length < opcaoCurso) {
                    Console.WriteLine("Opção de curso " + opcaoCurso + " para o candidato " + candidato.Chave + " inexistente.");
                    continue;
                  }
                  //Tenta empilhar o candidato no curso desejado, se não der estouro de pilha a vaga existe, do contrario tenta outro curso
                  try {
                    cursos[opcaoCurso].Candidatos.Empilhar(candidato);
                    Console.WriteLine("Candidato " + candidato.Chave + " adicionado ao curso " +opcaoCurso);
                  } catch (StackOverflowException soe) {
                    Console.WriteLine("Curso " + opcaoCurso + " lotato. Verificando outra vaga para o candidato " + candidato.Chave);
                  }
                }

            }

            //Mostra os Canditados nos cursos
            for (int j = 0; j < cursos.Length; j++)
            {
              Console.WriteLine("--------------------------------");
              Console.WriteLine("Candidatos e suas notas inscritos no curso " + curso.Id);
              Console.WriteLine("--------------------------------");
              while(!cursos[j].Candidatos.empty())
              {
                Candidato candidato = cursos[j].Candidatos.Desempilhar();

                Console.Write("candidato:" + candidato.Chave + " nota:" + candidato.NotaFinal + " , ");
              }
              Console.WriteLine("--------------------------------");
            }

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

       public Fila(T[] itens)
	   {
		  fim =
		  tamanhoFila =
		  tamanho = itens.Length;
		  
		  itens = itens;
	   }

       public boolean empty()
       {
         return tamanho == 0;
       }

	   public void Emfileirar(T item)
	   {
		  if((tamanhoFila + 1) > tamanho)  throw new StackOverflowException();

		  int fim = (fim+1%tamanho);
		  itens[fim] = item;
		  tamanhoFila++;
	   }

	   public T Desenfileirar()
	   {

		  if(tamanhoFila - 1 < 0)
		  {
		     throw new InvalidOperationException("A pilha esta vazia");
		  }
		  else
		  {
		  	try {
			  	tamanhoFila--;
				return itens[inicio];
			}
	        finally
        	{
				inicio = (inicio+1%tamanho);
			}
		  }
	   }
	}

    //Pilha
	public class Pilha<T>
	{
	   readonly int tamanho;
	   int posicao = 0;
	   T[] itens;

	   public Pilha(int size)
	   {
		  tamanho = size;
		  itens = new T[tamanho];
	   }
     public boolean empty()
     {
       return posicao > 0;
     }
	   public void Empilhar(T item)
	   {
		  if(posicao >= tamanho)  throw new StackOverflowException();

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
