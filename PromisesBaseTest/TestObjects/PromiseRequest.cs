﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termine.Promises.Interfaces;

namespace PromisesBaseTest
{
    public class PromiseRequest: IAmAPromiseRequest
    {
        public string Claim { get; set; }
    }
}
