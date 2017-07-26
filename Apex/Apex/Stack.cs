

namespace Apex.Apex
{
    public class Stack<T>
    {
        readonly global::System.Collections.Generic.Stack<T> _Stack;

        public Stack()
        {
            _Stack = new global::System.Collections.Generic.Stack<T>();
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Stack.Clone");
        }

        public bool Empty()
        {
            _Stack.Clear();
            return true;
        }

        public T Peek()
        {
            return _Stack.Peek();
        }

        public T Pop()
        {
            if (_Stack.Count == 0) throw new EmptyStackException();
            else
            {
                return _Stack.Pop();
            }
        }

        public void Push(T item)
        {
            _Stack.Push(item);
        }
    }
}