using System.Drawing;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Components;

namespace ReSharperPlugin.SharpCoachPlugin.Ui.ToolWindows
{
    public class MapModelsLogsToolWindow
    {
        public static void Show(IMappingResultsStorage mappingResultsStorage)
        {
            Shell.Instance.GetComponent<IToolWindowHost>().Show(GetToolWindow);

            BeToolWindow GetToolWindow(Lifetime lifetime)
            {
                var content = GetContent(mappingResultsStorage);
                
                return BeControls.GetToolWindow(
                    title: "Map Models Logs".GetBeLabel(),
                    toolWindowId: "MapModelsAction.MapLogs.ToolWindow", 
                    content: content,
                    lifetime: lifetime,
                    isSingleInstance: true,
                    position: BePosition.RIGHT);
            }
        }

        private static BeControl GetContent(IMappingResultsStorage mappingResultsProvider)
        {
            var toolWindow = BeControls.GetGrid();
            
            var multilineLabel = BeControls.GetMultilineLabel("This page shows results of mapping classes operation. Be sure to check, if classes were mapped correctly, otherwise you will lose some of properties.", "\n");
            toolWindow.AddElement(multilineLabel);

            var operationNumber = 1;
            var mappingTable = BeControls.GetGrid();
            foreach (var mappingResult in mappingResultsProvider)
            {
                var operationGrid = BeControls.GetGrid();

                var operationHeader = BeControls.GetHeader($"Mapping Operation #{operationNumber++} Results");
                operationGrid.AddElement(operationHeader);
                
                var operationInfo = $"[{mappingResult.OperationDate}] Mapping '{mappingResult.InputClassName}' to '{mappingResult.OutputClassName}'".GetBeLabel();
                operationGrid.AddElement(operationInfo);

                if (mappingResult.InputClassErrorPropertyNames.IsNullOrEmpty() && mappingResult.OutputClassErrorPropertyNames.IsNullOrEmpty())
                {
                    // if all went well - just show a success message
                    var successLabel = "Successful mapping operation".GetBeLabel().WithColor(Color.ForestGreen);
                    operationGrid.AddElement(successLabel);
                }
                else
                {
                    // If errors occured while mapping - drawing an errors properties table
                    var unsuccessfulLabel = "Unsuccessful mapping!".GetBeLabel().WithColor(Color.DarkRed);
                    var tableInfoLabel = "Here is a table of failed to map properties".GetBeLabel();
                    operationGrid.AddElement(unsuccessfulLabel);
                    operationGrid.AddElement(tableInfoLabel);

                    var propertiesTable = BeControls.GetGrid(GridOrientation.Horizontal);

                    // left column - input class failed to map properties
                    var inputPropertiesGrid = BeControls.GetGrid();
                    inputPropertiesGrid.AddElement("Input class properties".GetBeLabel().WithCustomTextSize(BeFontSize.BIGGER));
                    foreach (var errorInputProperty in mappingResult.InputClassErrorPropertyNames)
                    {
                        var errorPropertyLabel = errorInputProperty.GetBeLabel().InBorder(BeShowBorders.All);
                        inputPropertiesGrid.AddElement(errorPropertyLabel);
                    }
                
                    // right column - output class failed to map properties
                    var outputPropertiesGrid = BeControls.GetGrid();
                    outputPropertiesGrid.AddElement("Output class properties".GetBeLabel().WithCustomTextSize(BeFontSize.BIGGER));
                    foreach (var errorOutputProperty in mappingResult.OutputClassErrorPropertyNames)
                    {
                        var errorPropertyLabel = errorOutputProperty.GetBeLabel().InBorder(BeShowBorders.All);
                        outputPropertiesGrid.AddElement(errorPropertyLabel);
                    }

                    propertiesTable.AddElement(inputPropertiesGrid);
                    propertiesTable.AddElement(outputPropertiesGrid);

                    operationGrid.AddElement(propertiesTable);   
                }

                toolWindow.AddElement(operationGrid);
            }

            toolWindow.AddElement(mappingTable);
            return BeControls.GetScrollablePanel(toolWindow, scrollbarPolicy: BeScrollbarPolicy.VERTICAL);
        }
    }
}