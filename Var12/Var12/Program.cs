using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Var12
{
    abstract class Element 
    {
        protected bool state; // отвечает за текущее состояние
        int id; // идентификационный номер
        bool interacted; // отвечает за то, есть ли вывод в другой элемент
        protected int links_count; // колличество связей
        int[] connected_id = new int[10];// колличество связей
        public Element(int _id, bool _state = false, int _links_count = 0, params int[] _connected_id) 
        {
            interacted = false;
            state = _state;
            id = _id;
            links_count = _links_count;
            if (links_count > 10) links_count = 10;
            for (int i = 0; i < links_count; i++) connected_id[i] = _connected_id[i];
        }

        public void interact() { interacted = true; }
        public bool getInteractness() { return interacted; }
        public int getID() { return id; }
        public int getLC() { return links_count; }
        public bool getState() { return state; }
        public int[] getLinksID() { return connected_id; }
        public void linkTo(int _id) { if (links_count == 10) links_count = 9; connected_id[links_count] = _id; links_count++;  }
        public void removeLink(int _id) {connected_id[Array.FindIndex(connected_id, x => x == _id)] = -1; }
        public override string ToString()
        {
            string str = "";
            if (links_count == 0) str += " no links ";
            for (int i = 0; i < links_count; i++) str += connected_id[i].ToString() + " ";
            str += state.ToString();
            if (interacted) str += " соединён с другим элементом схемы";
            return str;
        }
        public abstract bool doWork(params bool[] _connected_states); // обработка входных значений
    }
    class OR : Element 
    {
        public OR(int _id, bool _state = false, int _links_count = 0, params int[] _connected_id) : base(_id, _state, _links_count, _connected_id) { }
        public override bool doWork(params bool[] _connected_states)
        {
            state = false;
            for (int i = 0; i < links_count;i++) 
            {
                if (_connected_states[i]) { state = true; return state; }
            }
            return state;   
        }
    }
    class ZERO : Element
    {
        public ZERO(int _id, bool _state = false, int _links_count = 0, params int[] _connected_id) : base( _id, _state, _links_count, _connected_id) { }
        public override bool doWork(params bool[] _connected_states)
        {
            state = false;
            return state;
        }
    }
    class ONE : Element
    {
        public ONE(int _id, bool _state = false, int _links_count = 0, params int[] _connected_id) : base( _id, _state, _links_count, _connected_id) { }
        public override bool doWork(params bool[] _connected_states)
        {
            state = true;
            return state;
        }
    }
    class AND : Element
    {
        public AND(int _id, bool _state = false, int _links_count = 0, params int[] _connected_id) : base(_id, _state, _links_count, _connected_id) { }
        public override bool doWork(params bool[] _connected_states)
        {
            state = true;
            for (int i = 0; i < links_count; i++)
            {
                if (!_connected_states[i]) { state = false; return state; }
            }
            return state;
        }
    }
    class Scheme 
    {
        List<Element> elements;
        int idcounter;
        enum TypeOfElement
        { 
            ZERO,
            ONE,
            OR,
            AND
        }
        public Scheme() 
        {
            elements = new List<Element>();
            idcounter = 0;
        }
        public void AddElement(int mode = 0) 
        {
            switch (mode)
            {
                case (int)TypeOfElement.ZERO:
                    elements.Add(new ZERO(idcounter++));
                    return;
                case (int)TypeOfElement.ONE:
                    elements.Add(new ONE(idcounter++));
                    return;
                case (int)TypeOfElement.OR:
                    elements.Add(new OR(idcounter++));
                    return;
                case (int)TypeOfElement.AND:
                    elements.Add(new AND(idcounter++));
                    return;
                default:
                    return;
            }
        }
        public void LinkTo(int src, int dst) 
        {
            try
            {
                elements.Find(x => x.getID() == dst).linkTo(elements.Find(x => x.getID() == src).getID());
                elements.Find(x => x.getID() == src).interact();
            }
            catch 
            {
                Console.WriteLine("Ошибка связки");
            }
        }
        public bool RunAndOutput(int id) 
        {
            Element a = elements.Find(x => x.getID() == id);
            bool[] inputs = new bool[a.getLC()];
            for (int i = 0; i < a.getLC(); i++)
                inputs[i] = RunAndOutput(a.getLinksID()[i]);
            return a.doWork(inputs);
        }
        public bool[] FullRun() 
        {
            bool[] outputs = new bool[elements.FindAll(x => x.getInteractness()).Count];
            int i = 0;
            elements.FindAll(x => x.getInteractness()).ForEach(x => outputs[i++] = RunAndOutput(x.getID()));
            return outputs;
        }
        public override string ToString() 
        {
            string str ="номера в схеме\n";
            elements.ForEach(x => str += x.getID().ToString() + "<-" + x.ToString() + "\n");
            return str;
        }

    }
    class Program
    {
        static void MainMenu() 
        {
            Console.WriteLine("+-----<Выберите операцию>-----+");
            Console.WriteLine("|1. Прочитать схему из файла  |");
            Console.WriteLine("|2. Прочитать схему из консоли|");
            Console.WriteLine("|3. Вывести информацию о схеме|");
            Console.WriteLine("|4. Вывод всех выходов схемы  |");
            Console.WriteLine("|5. Вывод выхода схемы по id  |");
            Console.WriteLine("|6. Выход                     |");
            Console.WriteLine("+-----------------------------+");
        }
        static void Body() { };
        static void Main(string[] args)
        {
            Scheme scheme = new Scheme();
            scheme.AddElement(0);
            scheme.AddElement(1);
            scheme.AddElement(2);
            scheme.AddElement(3);



            Console.WriteLine(scheme.ToString());
            bool[] arr = scheme.FullRun();
            for (int i = 0; i < arr.Length; i++) Console.WriteLine(arr[i].ToString() + " ");
            Console.ReadKey();
        }
    }
}
