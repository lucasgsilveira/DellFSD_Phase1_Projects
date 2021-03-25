using System;
using System.Collections.Generic;
using System.Text;

namespace Phase1Section4._7b
{
    public class SingleLinkedList
    {
        private Node root = null;
        private Node current = null;
        private int length;

        public Node Root
        {
            get { return this.root; }
            set { this.root = value; }
        }
