

using System;
using System.Collections;
using System.Globalization;

namespace AppListasFilasPilha
{
    public struct Candidato
    {
        public int Chave;
        public decimal NotaFinal;
        public int[] OpcaoCurso;

        public Candidato(int chave, decimal nota, int[] opcoes)
        {
            Chave = chave;
            NotaFinal = nota;
            OpcaoCurso = opcoes;
        }
        public void Add(Candidato[] lista)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].Chave.Equals(0))
                {
                    lista[i] = this;
                    break;
                }
            }
        }
        public void Remove(Candidato[] lista)
        {
            for (int i = lista.Length; i > 0; i--)
            {
                if (lista[i].Chave != 0)
                {
                    lista[i].Chave = 0;
                    lista[i].NotaFinal = 0;
                    lista[i].OpcaoCurso = new int[]{};
                    break;
                }
            }
        }

        public void RemoveIndex(Candidato[] lista, int index)
        {
            lista[index].Chave = 0;
            lista[index].NotaFinal = 0;
            lista[index].OpcaoCurso = new int[] { };    
        }

        public void ChangeIndex(Candidato[] lista, int index,int indexNew)
        {
            lista[indexNew].Chave = lista[index].Chave;
            lista[indexNew].NotaFinal = lista[index].NotaFinal;
            lista[indexNew].OpcaoCurso = lista[index].OpcaoCurso;
        }

        public Candidato[] OrdenaNotas(Candidato[] lista,string direction)
        {
            
            Candidato[] listaAux = new Candidato[lista.Length];
            if (lista.Length > 1)
            {
                for (int i = 0; i < lista.Length; i++)
                {
                    int index = i;
                    decimal maxNota = lista[index].NotaFinal;
                    for (int j = i + 1; j < lista.Length; j++)
                    {
                        if (lista.Length >= j)
                        {
                            if (direction == "DESC")
                            {
                                if (maxNota < lista[j].NotaFinal)
                                {
                                    maxNota = lista[j].NotaFinal;
                                    index = j;
                                }
                            }
                            else
                            {
                                if (maxNota > lista[j].NotaFinal)
                                {
                                    maxNota = lista[j].NotaFinal;
                                    index = j;
                                }
                            }
                        }
                    }
                    lista[index].Add(listaAux);
                    lista[index].ChangeIndex(lista,i,index);
                   
                }
            }
            return listaAux;

        }
        
    }

    public struct Curso
    {
        public int Id;
        public String Nome;
        public Fila<T> vagas;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //cria lista com capacidade de 10 canditados
            Candidato[] lista = new Candidato[5];
           
            //carrega candidatos
            Candidato c1 = new Candidato(1,9, new int[] {1,2,3 });
            Candidato c2 = new Candidato(2, 7, new int[] { 1, 2, 4 });
            Candidato c3 = new Candidato(3, 5, new int[] { 2, 2, 4 });
            Candidato c4 = new Candidato(4, 10, new int[] { 4, 1, 2 });
            Candidato c5 = new Candidato(5, 9, new int[] { 4, 5, 3 });
            c1.Add(lista);
            c2.Add(lista);
            c3.Add(lista);
            c4.Add(lista);
            c5.Add(lista);

           Candidato[] listaOdenada =   c5.OrdenaNotas(lista,"DESC");

            foreach (var item in listaOdenada)
            {
                Console.WriteLine(item.NotaFinal);
            }
            Console.Read();

        }
    }

    //Fila
	public class Fila<T>
	{
	   readonly int tamanho; 
	   int inicio = 0;
	   int fim = 0;
	   T[] itens; 

	   public Fila(int size)
	   {
		  tamanho = size + 1;
		  itens = new object[tamanho];
	   }
	   public void Emfileirar(T item)
	   {
		  int proximo = (fim+1%tamanho);
		  if(next == inicio)  throw new StackOverflowException();       

		  itens[fim] = item;
		  fim = proximo;
	   }
	   public T Desenfileirar()
	   {

		  if(inicio == fim)
		  {
		     throw new InvalidOperationException("A pilha esta vazia");
		  }
		  else
		  {
		  	try {
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
