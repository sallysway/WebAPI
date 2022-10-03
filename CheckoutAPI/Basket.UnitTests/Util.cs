using Application;
using AutoMapper;

namespace Basket.UnitTests
{
    public static  class Util
    {
        public static Mapper BuildMapper()
        {
            var profile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(profile));
            return new Mapper(mapperConfig);
        }
    }
}
