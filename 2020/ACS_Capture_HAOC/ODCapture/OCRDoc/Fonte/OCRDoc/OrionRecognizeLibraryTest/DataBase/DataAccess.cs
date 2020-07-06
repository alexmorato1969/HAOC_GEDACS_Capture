using OrionRecognizeLibraryTest.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionRecognizeLibraryTest
{
    public class DataAccess
    {
        public OCR_MappingZone OCR_MappingZone_Update(OCR_MappingZone oCR_MappingZone, int mapIndex,int mapWidth,int mapHeigth, int DocId)
        {
            try
            {
                using (var db = new OCRDocEntities())
                {
                            
                    var oldzone = db.OCR_MappingZone.FirstOrDefault(x => x.ZON_Id == oCR_MappingZone.ZON_Id);

                    if (oldzone == null)
                    {
                        var mapR = from mp in db.OCR_Mapping
                                   join mt in db.OCR_MappingXTypeDocument on mp.MAP_Id equals mt.MXT_MAP_Id
                                   where mp.MAP_Index == mapIndex &&
                                         mt.MXT_TDC_Id == DocId
                                   select mp;

                        var map = mapR.FirstOrDefault();
                        
                        if (map == null)
                        {

                            map = new OCR_Mapping();
                            map.MAP_Index = mapIndex;

                            db.OCR_Mapping.Add(map);
                            
                            db.SaveChanges();

                            db.OCR_MappingXTypeDocument.Add(new OCR_MappingXTypeDocument() { MXT_MAP_Id = map.MAP_Id, MXT_TDC_Id = DocId });
                            db.SaveChanges();
                        }

                        db.Entry(map).Property("MAP_Width").CurrentValue = mapWidth;
                        db.Entry(map).Property("MAP_Height").CurrentValue = mapHeigth;

                        
                        oCR_MappingZone.ZON_MAP_Id = map.MAP_Id;

                        db.OCR_MappingZone.Add(oCR_MappingZone);
                        db.SaveChanges();
                    }
                    else
                    {
                        var old_oCR_MappingZone = from mz in db.OCR_MappingZone
                                                  where mz.ZON_Id == oCR_MappingZone.ZON_Id
                                                  select mz;

                        db.Entry(old_oCR_MappingZone.FirstOrDefault()).CurrentValues.SetValues(oCR_MappingZone);
                        db.SaveChanges();
                    }


                
                }
                return oCR_MappingZone;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OCR_MappingZone[] GetZones(int DocId, int MapIndex)
        {
            try
            {
                using (var db = new OCRDocEntities())
                {
                    var result = from mz in db.OCR_MappingZone
                                 join mt in db.OCR_MappingXTypeDocument on mz.ZON_MAP_Id equals mt.MXT_MAP_Id
                                 join mp in db.OCR_Mapping on mz.ZON_MAP_Id equals mp.MAP_Id
                                 where mp.MAP_Index == MapIndex &&
                                       mt.MXT_TDC_Id == DocId
                                 select mz;

                    return result.ToArray();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public OCR_Mapping Get_OCR_Mapping(int mAP_Id)
        {
            try
            {
                using (var db = new OCRDocEntities())
                {
                    var result = from mp in db.OCR_Mapping
                                 where mp.MAP_Id == mAP_Id
                                 select mp;

                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OCR_MappingZone Get_OCR_MappingZone(int zON_Id)
        {
            try
            {
                using (var db = new OCRDocEntities())
                {
                    var result = from mz in db.OCR_MappingZone
                                 where mz.ZON_Id == zON_Id
                                 select mz;

                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
