﻿using System.Collections.Generic;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericUserIdentity: IAmAPromiseUser
    {
        public GenericUserIdentity()
        {
            
        }

        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}