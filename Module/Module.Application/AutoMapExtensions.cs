using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Module.Application
{
    public static class AutoMapExtensions
    {
        static AutoMapExtensions()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMissingTypeMaps = true;
            });
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }

}
