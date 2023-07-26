using System;

namespace KanbanApiEfc.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(String msg) : base(msg) { }
    }
}
