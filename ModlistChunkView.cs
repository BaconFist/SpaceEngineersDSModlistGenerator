using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngineersDSModlistGenerator
{
    class ModlistChunkView
    {

        public StringBuilder renderChunk(List<String> modChunk, int currentChunkId, int chunkMax)
        {
            StringBuilder content = new StringBuilder();
            if(currentChunkId == 0)
            {
                content.AppendLine("[i]Aktualisiert am "+ DateTime.Now.ToShortDateString() + "[/i]");
            }
            content.AppendLine("[h1]Modliste " + (currentChunkId + 1 ).ToString() + "/" + chunkMax.ToString() + "[/h1]");

            content.AppendLine("[list]");
            for(int i = 0; i < modChunk.Count; i++)
            {
                content.AppendLine("[*]http://steamcommunity.com/sharedfiles/filedetails/?id=" + modChunk[i]);
            }
            content.AppendLine("[/list]");
            return content;
        }
        
    }
}
