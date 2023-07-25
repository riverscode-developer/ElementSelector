using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;


namespace ElementSelector
{
    [Transaction(TransactionMode.Manual)]
    public class CmdTotalVolume : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;


            //FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_StructuralFoundation);
            //collector.WhereElementIsNotElementType();
            // collector.Where(el => el.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble() > 0);

            //List<Element> elementList = collector.ToElements().ToList();


            // elementList = [element1, element2, element3, element4, element5, element6, element7, element8, element9, element10]

            // hacer acciones repetivas hasta que se cumpla una condición o recorrer cierta cantidad de elementos
            // While, Do While, For, Foreach

            #region While
            //int i = 0;
            //double totalVolume = 0; // variable acumuladora
            //while (i < elementList.Count)
            //{
            //    // Se ejecutará hasta que i sea menor a la cantidad de elementos
            //    double volume = elementList[i].get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();

            //    totalVolume += volume;   // totalVolume = totalVolume + volume;

            //    // Aumentar el valor de i
            //    i++; //i = i + 1;
            //}
            #endregion

            #region For 
            //for (int y = 0; y < elementList.Count; i++)
            //{
            //    double volume = elementList[i].get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();
            //    totalVolume = totalVolume + volume;
            //}
            #endregion

            #region Foreach
            //foreach (Element element in elementList)
            //{
            //    double volume = element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble();
            //    if (volume > 10)
            //    {
            //        totalVolume += volume;
            //    }
            //}
            #endregion


            #region Utilizando sintanxis de consulta
            //double totalVolume2 = (from element in elementList
            //                       where element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble() > 10
            //                       select element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble()).Sum();
            #endregion

            #region Utilizando expresiones lambda
            //double totalVolume3 = elementList
            //                        .Where(element => element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble() > 10)
            //                        .Select(element => element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble())
            //                        .Sum();
            #endregion

            //var data = (from e in elementList
            //                        select e.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble()).ToList();



            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Level));

            List<Element> elementList = collector.ToElements().ToList();

            List<Element> levelList = (from el in elementList
                                       orderby el.get_Parameter(BuiltInParameter.LEVEL_ELEV).AsDouble() descending
                                       select el).ToList();



            return Result.Succeeded;
        }
    }
}
