using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mobilecenterpush.Tags
{
    public static class CosmosDBRepository//<T> where T : class
    {

        private static DocumentClient client;

        public static async Task<T> GetItemAsync<T>(string _collectionID, string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(Keys.CosmosDB.DatabaseId, _collectionID, id));
                return (T)(dynamic)document;
            }
            catch (Exception e)
            {
                //if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                //{
                //    return default(T);
                //}
                //else
                //{
                //    throw;
                //}

                throw;
            }
        }

        public static async Task<IEnumerable<T>> GetItemsFilteredAsync<T>(string _collectionID, Expression<Func<T, bool>> predicate)
        {

            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(Keys.CosmosDB.DatabaseId, _collectionID),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<IEnumerable<T>> GetItemsAsync<T>(string _collectionID)
        {

            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(Keys.CosmosDB.DatabaseId, _collectionID),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<Document> CreateItemAsync<T>(string _collectionID, T item)
        {
            try
            {
                return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(Keys.CosmosDB.DatabaseId, _collectionID), item);
                //return (T)(dynamic)res;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static async Task<Document> UpdateItemAsync<T>(string _collectionID, string id, T item)
        {
            try
            {
                return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Keys.CosmosDB.DatabaseId, _collectionID, id), item);
                //return (T)(dynamic)res;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Keys.CosmosDB.DatabaseId, Keys.CosmosDB.TagsCollectionId, id));
        }

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(Keys.CosmosDB.Endpoint), Keys.CosmosDB.AuthKey);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(Keys.CosmosDB.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = Keys.CosmosDB.DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(Keys.CosmosDB.DatabaseId, Keys.CosmosDB.TagsCollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(Keys.CosmosDB.DatabaseId),
                        new DocumentCollection { Id = Keys.CosmosDB.TagsCollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
