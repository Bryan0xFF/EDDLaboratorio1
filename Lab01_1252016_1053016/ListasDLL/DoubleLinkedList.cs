using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasDLL
{
    class DoubleLinkedList<T> where T: IComparable<T>
    {
        private Node<T> header = null;//Referencia
        private Node<T> trailer = null;
        private int tamanio = 0;

        public DoubleLinkedList()
        {
            header = new Node<T>(default(T), null, null);
            trailer = new Node<T>(default(T), header, null);
            header.setNext(trailer);
        }

        public int size()
        {
            return tamanio;
        }

        public bool isEmpty()
        {
            return tamanio == 0;
        }

        public  T first()
        {
            if (isEmpty())
                return default(T);
            return header.getNext().getElement();
        }

        public T last()
        {
            if (isEmpty())
                return default(T);
            return trailer.getPrev().getElement();
        }

        public void addFirst(T t)
        {
            addBetween(t, header, header.getNext());
        }

        public void addLast(T t)
        {
            addBetween(t, trailer.getPrev(), trailer);
        }

        public T removeFirst()
        {
            if (isEmpty())
                return default(T);
            return remove(header.getNext());
        }
        public T removeLast()
        {
            if (isEmpty())
                return default(T);
            return remove(trailer.getPrev());
        }

        private void addBetween(T t, Node<T> predecessor, Node<T> successor)
        {
            Node<T> newest = new Node<T>(t, predecessor, successor);
            predecessor.setNext(newest);
            successor.setPrev(newest);
            tamanio++;
        }

        private T remove(Node<T> node)
        {
            Node<T> predecessor = node.getPrev();
            Node<T> successor = node.getNext();
            predecessor.setNext(successor);
            successor.setPrev(predecessor);
            tamanio--;
            return node.getElement();
        }

    }
}
