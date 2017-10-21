using EmployeePayslipCalculator.WPFApp.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayslipCalculator.WPFApp.Services
{


    public class WebApiService
    {
        /// <summary>
        /// Private singleton field.
        /// </summary>
        private static WebApiService _instance;

        public static WebApiService Instance = _instance ?? (_instance = new WebApiService());
        

        /// <summary>
        /// Maximum number of Http Clients that can be pooled.
        /// </summary>
        private const int DefaultPoolSize = 10;
        private SemaphoreSlim _semaphore = null;
        /// <summary>
        /// Private instance field.
        /// </summary>
        private ConcurrentQueue<HttpClient> _httpClientQueue = null;

        public WebApiService()
            : this(DefaultPoolSize)
        {
        }

        public WebApiService(int poolSize)
        {
            _semaphore = new SemaphoreSlim(poolSize);
            _httpClientQueue = new ConcurrentQueue<HttpClient>();
        }




        private HttpClient GetHttpClientInstance()
        {
            HttpClient client = null;
            // Try and get HttpClient from the queue
            if (!_httpClientQueue.TryDequeue(out client))
            {
                client = new HttpClient();
            }
            return client;
        }

        #region Get
        public async Task<HttpResponseMessage> GetResponseAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            HttpClient client = null;
            try
            {
                client = GetHttpClientInstance();
                return await client.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Add the HttpClient instance back to the queue.
                if (client != null)
                {
                    _httpClientQueue.Enqueue(client);
                }
                _semaphore.Release();
            }
        }


        public async Task<string> GetStringAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await GetResponseAsync(uri, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await GetResponseAsync(uri, cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return SerializerHelper.Create().FromJson<T>(jsonString);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Post
        public async Task<HttpResponseMessage> PostResponseAsync(Uri uri, HttpContent httpContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            HttpClient client = null;
            try
            {
                client = GetHttpClientInstance();
                return await client.PostAsync(uri, httpContent, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Add the HttpClient instance back to the queue.
                if (client != null)
                {
                    _httpClientQueue.Enqueue(client);
                }
                _semaphore.Release();
            }
        }

        public async Task<string> PostJsonDataAndReturnStringAsync<TRequest>(Uri uri, TRequest postData, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var content = new StringContent(SerializerHelper.Create().ToJson<TRequest>(postData), Encoding.UTF8, "application/json");
                var response = await PostResponseAsync(uri, content, cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResponse> PostJsonData<TRequest, TResponse>(Uri uri, TRequest postData, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var jsonString = await PostJsonDataAndReturnStringAsync(uri, postData, cancellationToken);
                return SerializerHelper.Create().FromJson<TResponse>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PostFormDataAndReturnStringAsync(Uri uri, List<KeyValuePair<string, string>> httpContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await PostResponseAsync(uri, new FormUrlEncodedContent(httpContent), cancellationToken);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> PostFormDataAsync<T>(Uri uri, List<KeyValuePair<string, string>> httpContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await PostResponseAsync(uri, new FormUrlEncodedContent(httpContent), cancellationToken);
                response.EnsureSuccessStatusCode();
                // Sometimes the response should be decoded by the specific encoding.
                //var buffer = await response.Content.ReadAsBufferAsync();
                //var byteArray = buffer.ToArray();
                //var jsonString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                string jsonString = await response.Content.ReadAsStringAsync();
                return SerializerHelper.Create().FromJson<T>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }
}
