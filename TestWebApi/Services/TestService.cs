using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Services
{
    public class TestService
    {
        private readonly string config;
        private readonly ICollection<Test> list = new HashSet<Test>();
        public TestService(string config)
        {
            this.config = config;
            list = new List<Test>()
                    {
                        new Test { Id = 1, Name = $"Test 1 {config}", Mobile = "099-999-9999"},
                        new Test { Id = 2, Name = $"Test 2 {config}", Mobile = "099-999-9999"},
                        new Test { Id = 3, Name = $"Test 3 {config}", Mobile = "099-999-9999"},
                        new Test { Id = 4, Name = $"Test 4 {config}", Mobile = "099-999-9999"},
                        new Test { Id = 5, Name = $"Test 5 {config}", Mobile = "099-999-9999"}
                    };
        }
        public async Task<ICollection<Test>> GetTests()
        {
            return await Task.FromResult(list);
        }

        public async Task<Test> GetTestById(int id)
        {
            return await Task.FromResult(list.SingleOrDefault(test => test.Id == id));
        }
    }
}
