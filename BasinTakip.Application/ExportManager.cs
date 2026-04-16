using ClosedXML.Excel;
using JohnsonNet;
using BasinTakip.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Repository;

namespace BasinTakip.Application
{
    public class ExportManager
    {
        public void Save(string filePath)
        {
            throw new NotImplementedException();
        }

        public Stream EventReport(ReportInput input)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "Name", "Etkinlik Adı"},
                { "EventPlace", "Etkinlik Yeri"},
                { "EventTypeName", "Etkinlik Türü"},
                { "BeginDate", "Başlangıç Tarihi"},
                { "EndDate", "Bitiş Tarihi"},
                { "Count", "Tamas Kurulan Kişi sayısı"},
            };
            //if (input.BeginYear != null & input.BeginMonth != null)
            //{
            //    input.BeginDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);

            //}
            var result = IocManager.Resolve<IEventManager>().GetInclueded(1, int.MaxValue,input.BeginDate,input.EventPlace,input.EventType,input.OrderByColumn==null?"Id":input.OrderByColumn,input.OrderType,input.SearchText,input.BeginYear,input.BeginMonth);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        //PressMember Listeleme sayfası için export methodu
        public Stream PersonReport(PersonResult input)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "FirstName", "Ad"},
                { "LastName", "Soyad"},
                { "MobilePhone", "Cep Telefonu"},
                { "Email", "Email"},
                { "TaskName", "Görev Bilgileri"},
                { "EditionName", "Yayın"},
                { "CreatedAt", "kayıt Tarihi"},
            };
            if (input.BeginYear != null & input.BeginMonth != null)
            {
                input.ContactDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);
            }
            var result = IocManager.Resolve<IPersonManager>().GetInclueded(1, int.MaxValue,input.ContactDate, input.EditionId, input.TaskId,input.CityId, input.DistrictId
                , input.Block, input.orderByColumn == null ? "Id" : input.orderByColumn, input.OrderType==true?true:false,input.SearchText,input.BeginYear,input.BeginMonth);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        //Temas Kayıtları listeleme sayfası icin export methodu 
        public Stream ContactReport(ContactRecordResult input)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "EditionTypeName", "Yayın"},
                { "ContactTypeName", "Temas Şekli"},
                { "PressMemberName", "Basın Mensubu"},
                { "Notes", "Not"},
                { "LcvName", "LCV"},
                { "participationStatusName", "Katılım Durumu"},
                { "ContactDate", "Temas Tarihi"},
            };
            if (input.BeginYear != null & input.BeginMonth != null)
            {
                input.BeginDate = new DateTime((int)input.BeginYear, (int)input.BeginMonth, 1);
            }
            
            var result = IocManager.Resolve<IContactRecordManager>().GetInclueded(1, int.MaxValue,input.BeginDate ,input.EditionId, input.ContactTypeId, input.orderByColumn == null ? "Id" : input.orderByColumn, input.OrderType ==true?true:false
                , input.SearchText,input.BeginYear,input.BeginMonth);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        public Stream VehicleReport()
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "Marka", "Marka"},
                { "serial", "Seri"},
                { "Model", "Model"},
                { "Plate", "Plaka"},
                { "ModelDate", "Model Yılı"}
            };
            
            var result = IocManager.Resolve<IVehicleManager>().All();

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        public Stream EditionReport(EditionResult input)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "Name", "Yayın Adı"},
                { "EditionTypeName", "Yayın Türü"},
                { "Adress", "Adres"},
            };

            var result = IocManager.Resolve<IEditionManager>().GetInclueded(1,int.MaxValue,input.OrderByColumn==null?"Id":input.OrderByColumn,input.OrderType,input.SearchText);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }


        public Stream ContactBackPressmemberReport(int id)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
                { "ContactBackName", "Temas Şekli"},
                { "EditionName", "Yayın"},
                { "Note", "Not"},
                { "lcvName", "LCV"},
                { "particialName", "Katılım Durumu"},
                { "date", "Temas Başlangıç Tarihi"},
                { "LastContactDate", "Temas Bitiş Tarihi"},
            };

            var columnNameDictionary2 = new Dictionary<string, string>
            {
                { "Ad", "Ad"},
                { "Soyad", "Soyad"},
                { "MobilePhone", "Cep Telefonu"},
                { "Email", "E-Mail"},
                { "Email2", "E-Mail2"},
                { "PressMemberBirthDate", "Doğum Tarihi"},
                { "Adress", "Adres"},
                { "Note", "Note"},
                { "About", "Hakkında"},
                { "TaskName", "Görev Bilgisi"},
                { "CountryName", "İl"},
                { "districtName", "İlçe"},
                { "EditionNames", "Yayınlar"},
            };


            var result = IocManager.Resolve<IPersonManager>().GetFilterContactRecord(id);
            
            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");
                var sheet2 = workBook.AddWorksheet("Sheet2");
                int satir = 1;
                {
                    int columnIndex = 1;
                   
                    foreach (var column in columnNameDictionary2)
                    {
                        sheet.Cell(satir, columnIndex).Value = column.Value;
                        columnIndex++;
                    }

                }
                using (IocManager.BeginScope())
                {
                  var  test = IocManager.Resolve<IPersonRepository>().PressMemberDetail(id);
                    for (int rowIndex = 0; rowIndex < 1; rowIndex++)
                    {
                        var row = test[rowIndex];

                        int columnIndex = 1;

                        foreach (var column in columnNameDictionary2)
                        {
                            sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                            columnIndex++;
                        }
                    }
                }

                 int columnIndex2 = 1;
                foreach (var column in columnNameDictionary)
                {
                  
                    sheet.Cell(4, columnIndex2).Value = column.Value;
                    columnIndex2++;
                }

                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 5, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }
                }



                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        public Stream ContactBackEventReport(int id)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
               
                { "firstNamelastName", "Basın Mensubu"},
                { "EditionName", "Yayın"},
                { "Note", "Not"},
                { "lcvName", "LCV"},
                { "particialName", "Katılım Durumu"},
                { "date", "Temas Tarihi"},
            };

            var result = IocManager.Resolve<IEventManager>().GetFilterContactRecord(id);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }

        public Stream ContactBackVehicleReport(int id)
        {
            var memoryStream = new MemoryStream();

            var columnNameDictionary = new Dictionary<string, string>
            {
               { "firstNamelastName", "Basın Mensubu"},
                { "EditionName", "Yayın"},
                { "Note", "Not"},
                { "lcvName", "LCV"},
                { "particialName", "Katılım Durumu"},
                { "date", "Temas Tarihi"},
            };

            var result = IocManager.Resolve<IVehicleManager>().GetFilterContactRecord(id);

            using (var workBook = new XLWorkbook())
            {
                var sheet = workBook.AddWorksheet("Sheet1");

                {
                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(1, columnIndex).Value = column.Value;
                        columnIndex++;
                    }
                }
                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    var row = result[rowIndex];

                    int columnIndex = 1;
                    foreach (var column in columnNameDictionary)
                    {
                        sheet.Cell(rowIndex + 2, columnIndex).Value = JohnsonManager.Reflection.GetPropertyValue(row, column.Key);

                        columnIndex++;
                    }

                }
                workBook.SaveAs(memoryStream);
                memoryStream.Position = 0;
            }

            return memoryStream;
        }
    }
}
