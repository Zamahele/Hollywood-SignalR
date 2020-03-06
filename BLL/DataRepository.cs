using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BLL
{
    public class DataRepository<T> 
    {


        public HttpResponseMessage GetAllJsonResult(string requestUri)
        {
            var response = GlobalVariables.WebApiClient.GetAsync(requestUri).Result; ;    
            return response;
        }

        public IEnumerable<T> GetAll(string requestUri)
        {
            var response = GlobalVariables.WebApiClient.GetAsync(requestUri).Result;
            if (!response.IsSuccessStatusCode)
            {
            }
            var list = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
            return list;

        }

        public T GetById(string requestUri, int? id)
        {
            var response = GlobalVariables.WebApiClient.GetAsync(requestUri + "/" + id).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            var result = response.Content.ReadAsAsync<T>().Result;
            return result;
        }


        public T GetById(string requestUri, string id)
        {
            var response = GlobalVariables.WebApiClient.GetAsync(requestUri + "/" + id).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            var result = response.Content.ReadAsAsync<T>().Result;
            return result;
        }

        public void Edit(string requestUri, T target, int id)
        {
      
            var response = GlobalVariables.WebApiClient.PutAsJsonAsync(requestUri + "/" + id, target).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
        }

        public void Save(string requestUri, T target)
        {
            
            var response = GlobalVariables.WebApiClient.PostAsJsonAsync(requestUri, target).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
        }
        public void Delete(string requestUri, int? id)
        {
            var response = GlobalVariables.WebApiClient.DeleteAsync(requestUri + "/" + id).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
        }

     

    }
}
