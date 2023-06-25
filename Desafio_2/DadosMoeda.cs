using System.Collections.Generic;

namespace Desafio_2
{
    public class DadosMoeda
    {
        public class Name
        {
            public string common { get; set; }
            public string official { get; set; }
            public NativeName nativeName { get; set; }
        }

        public class NativeName
        {
            public Por por { get; set; }
        }

        public class Por
        {
            public string official { get; set; }
            public string common { get; set; }
        }

        public class Root
        {
            public Name name { get; set; }
        }
    }
}
