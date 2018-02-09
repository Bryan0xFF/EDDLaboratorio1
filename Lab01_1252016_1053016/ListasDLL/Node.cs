using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasDLL
{
    class Node<T>
    {       
		private T element;
        private Node<T> prev;//Anterior
        private Node<T> next;//Siguiente

        public Node(T t, Node<T> p, Node<T> n)
        {
            element = t;
            prev = p;
            next = n;
        }

        public T getElement()
        {
            return element;
        }

        public Node<T> getPrev()
        {
            return prev;
        }

        public void setPrev(Node<T> prev)
        {
            this.prev = prev;
        }

        public Node<T> getNext()
        {
            return next;
        }

        public void setNext(Node<T> next)
        {
            this.next = next;
        }


    }
}
