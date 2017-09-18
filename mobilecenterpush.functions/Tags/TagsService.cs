using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobilecenterpush.Model;

namespace mobilecenterpush.Tags
{
    class TagsService
    {
        static TagsService _instance;

        public static TagsService Instance
        {
            get
            {
                return _instance ?? (_instance = new TagsService());
            }

        }

        TagsService()
        {
            CosmosDBRepository.Initialize();
        }

        public async Task<IEnumerable<string>> GetUsersFromTagAsync(string tagId)
        {
            var existingTag = await CreateTagAsync(tagId);

            return existingTag?.Subscribers;
        }
        
        public async Task<Tag> CreateTagAsync(string tagId)
        {
            var existingTags = await CosmosDBRepository.GetItemsFilteredAsync<Tag>(Keys.CosmosDB.TagsCollectionId, x=>x.TagName==tagId);

            var existingTag = existingTags.FirstOrDefault();

            Tag newTag = new Tag();

            if(existingTag == default(Tag))
            {
                newTag = new Tag();
                newTag.TagId = Guid.NewGuid().ToString();
                newTag.TagName = tagId;
                newTag.id = newTag.TagId;

                var res = await CosmosDBRepository.CreateItemAsync(Keys.CosmosDB.TagsCollectionId, newTag);
                
                if(res!=null)
                    return newTag;
            }

            return existingTag;
        }

        public async Task<bool> AddSubscribersToTagAsync(string tagId, List<string> subscriberIds)
        {
            var existingTag = await CreateTagAsync(tagId);

            var newTag = existingTag;
            var guid = new Guid();
            foreach (var id in subscriberIds)
            {
                // CHECK TO SEE IF SUBSCRIBER ID IS A VALID GUID
                if (Guid.TryParse(id, out guid))
                {
                    if (!newTag.Subscribers.Contains(id))
                    {
                        newTag.Subscribers.Add(id);
                    }
                }
            }

            var res = await CosmosDBRepository.UpdateItemAsync(Keys.CosmosDB.TagsCollectionId, newTag.id.ToString(), newTag);
            if (res == null)
                return false;

            return true;
        }

        public async Task<bool> RemoveSubscribersFromTagAsync(string tagId, List<string> subscriberIds)
        {
            var existingTag = await CreateTagAsync(tagId);

            var newTag = existingTag;

            foreach (var id in subscriberIds) {
                if (newTag.Subscribers.Contains(id))
                {
                    newTag.Subscribers.Remove(id);
                }
            }

            var res = await CosmosDBRepository.UpdateItemAsync(Keys.CosmosDB.TagsCollectionId, newTag.id.ToString(), newTag);
            if (res == null)
                return false;
        
            return true;
        }


    }
}
