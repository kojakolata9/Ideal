using System;
using System.Collections.Generic;
using System.Text;

namespace Ideal.Common
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
