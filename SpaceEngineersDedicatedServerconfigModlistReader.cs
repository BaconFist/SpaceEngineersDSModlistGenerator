using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace SpaceEngineersDSModlistGenerator
{
    class SpaceEngineersDedicatedServerconfigModlistReader
    {
        const int CHUNK_SIZE = 10;

        private String filename;
        private List<String> modCollection;
        
        public SpaceEngineersDedicatedServerconfigModlistReader(String spaceEngineersDedicatedCfgFilename)
        {
            setFilename(spaceEngineersDedicatedCfgFilename);
            modCollection = new List<string>();
        }

        public void setFilename(String spaceEngineersDedicatedCfgFilename)
        {
            if (File.Exists(spaceEngineersDedicatedCfgFilename))
            {
                filename = spaceEngineersDedicatedCfgFilename;
                
            } else
            {
                filename = null;
                throw new FileNotFoundException("Unable to find Space Engineers Dedicated Server Configfile", spaceEngineersDedicatedCfgFilename);
            }
            
        }

        public void parseCfg()
        {
            if (!hasValidCfgFile())
            {
                throw new InvalidOperationException("No Valid Space Engineers Dedicated Server Configfile");
            }
            modCollection.Clear();
            XmlDocument Doc = new XmlDocument();
            XmlNamespaceManager NsMgr = new XmlNamespaceManager(Doc.NameTable);
            NsMgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            NsMgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
            Doc.Load(filename);
            XmlNodeList nodeList = Doc.SelectNodes("/MyConfigDedicated/Mods/unsignedLong", NsMgr);
            for(int i = 0; i < nodeList.Count; i++)
            {
                modCollection.Add(nodeList[i].InnerText);
            }
        }

        public int getChunkCount()
        {
            return Convert.ToInt32(Math.Ceiling(modCollection.Count / Convert.ToDouble(CHUNK_SIZE)));
        }

        public List<String> getChunk(int chunkNumber)
        {
            int chunkCount = getChunkCount();
            int start;
            int count;

            if (chunkNumber >= chunkCount)
            {
                throw new ArgumentOutOfRangeException("chunkNumber", chunkNumber, "Value not in range [0.." + (chunkCount - 1).ToString() + "]");
            }
            start = CHUNK_SIZE * chunkNumber;
            count = CHUNK_SIZE;
            if((start + CHUNK_SIZE) > modCollection.Count)
            {
                count = modCollection.Count - start;
            }

            return modCollection.GetRange(start, count);
        }

        public List<List<String>> getAllChunks()
        {
            List<List<String>> chunks = new List<List<string>>();
            for (int i = 0; i < getChunkCount(); i++)
            {
                chunks.Add(getChunk(i));
            }

            return chunks;
        }

        private bool hasValidCfgFile()
        {
            return File.Exists(filename);
        }

        public List<String> getModCollection()
        {
            return modCollection;
        }
    }
}
