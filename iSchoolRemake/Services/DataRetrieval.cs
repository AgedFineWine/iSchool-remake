using System.Net.Http.Headers;

namespace iSchoolRemake.Services
{
    public class DataRetrieval
    {
        //Task vs Thread
        /*
         * Task has async/await and a return value!
         * (no direct way to return from a thread)
         * task can do multiple things at once, thread can do one
         * can cancel a Task
         * 
         * Task is a higher level concept than thread.
         * Fancy thread with a callback
         */

        // d is the endpoint like 'about/' or 'people/'
        public async Task<string> GetData(string d)
        {
            // using statement - at the end of it automatically calls dispose method
            using (var client = new HttpClient())
            {
                // setup
                client.BaseAddress = new Uri("https://ischool.gccis.rit.edu/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );

                // try/catch
                try
                {
                    // we are going to get back a big ass string
                    HttpResponseMessage res = await client.GetAsync(d, HttpCompletionOption.ResponseHeadersRead);
                    // make sure we get a 200
                    res.EnsureSuccessStatusCode();
                    //go get it!
                    var data = await res.Content.ReadAsStringAsync();
                    //at this point, data is just a string...
                    return data;
                }
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    return "HttpReq: " + msg;
                }
                catch (Exception err)
                {
                    var msg = err.Message;
                    return "Ex: " + msg;
                }
            }
        }
    }
}
