using System.Collections.Generic;
using System.Drawing;
using JetBrains.Collections.Viewable;
using JetBrains.IDE.UI;
using JetBrains.IDE.UI.Extensions;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;
using ReSharperPlugin.SharpCoachPlugin.Core.Components;
using ReSharperPlugin.SharpCoachPlugin.Core.Models.OperationResults;

namespace ReSharperPlugin.SharpCoachPlugin.Ui.ToolWindows
{
    // ReSharper disable once InconsistentNaming
    public static class MapModelsLogsToolWindowUI
    {
        public static void Show(MappingResultsStorage mappingResultsStorage, bool isHidden = true)
        {
            var toolWindowHost = Shell.Instance.GetComponent<IToolWindowHost>();
            toolWindowHost.Show(GetToolWindow);

            BeToolWindow GetToolWindow(Lifetime tmpLifetime)
            {
                var content = GetContent(mappingResultsStorage);
                var toolWindowViewable = isHidden ? BeToolWindowState.Hidden : BeToolWindowState.Active;
                var viewableToolWindowProperty = new ViewableProperty<BeToolWindowState>(toolWindowViewable);  
            
                return BeControls.GetToolWindow(
                    title: "Map Models Logs".GetBeLabel(),
                    toolWindowId: "MapModelsAction.MapLogs.ToolWindow", 
                    content: content,
                    lifetime: tmpLifetime,
                    isSingleInstance: true,
                    position: BePosition.RIGHT,
                    state: viewableToolWindowProperty);
            }
        }

        private static BeControl GetContent(IEnumerable<MappingOperationResult> mappingOperationResults)
        {
            var toolWindow = BeControls.GetGrid();

            var multilineLabel = BeControls.GetMultilineLabel("This page shows results of mapping classes operation. Be sure to check, if classes were mapped correctly, otherwise you will lose some of properties.", "\n");
            toolWindow.AddElement(multilineLabel);

            var mappingTableGrid = BuildOperationsGrid(mappingOperationResults);
            
            toolWindow.AddElement(mappingTableGrid);
            return BeControls.GetScrollablePanel(toolWindow, scrollbarPolicy: BeScrollbarPolicy.VERTICAL);
        }

        private static BeGrid BuildOperationsGrid(IEnumerable<MappingOperationResult> mappingOperationResults)
        {
            var operationNumber = 1;
            var mappingTable = BeControls.GetGrid();
            foreach (var mappingResult in mappingOperationResults)
            {
                var operationGrid = BeControls.GetGrid();

                var operationHeader = BeControls.GetHeader($"Mapping Operation #{operationNumber++} Results");
                operationGrid.AddElement(operationHeader);
                
                var operationInfo = $"[{mappingResult.OperationDate}] Mapping '{mappingResult.InputClassName}' to '{mappingResult.OutputClassName}'".GetBeLabel();
                operationGrid.AddElement(operationInfo);

                if (mappingResult.FailedToMapPropertiesContainer.FromClassPropertyNames.IsNullOrEmpty() && mappingResult.FailedToMapPropertiesContainer.ToClassPropertyNames.IsNullOrEmpty())
                {
                    // if all went well - just show a success message
                    var successLabel = "Successful mapping operation".GetBeLabel();
                        // TODO fix System.MissingMethodException: Method not found jetbrains.platform.ide
                        // .WithColor(Color.Green);
                    operationGrid.AddElement(successLabel);
                }
                else
                {
                    // If errors occured while mapping - drawing an errors properties table
                    var unsuccessfulLabel = "Unsuccessful mapping!".GetBeLabel();
                        // TODO resolve dll .WithColor(Color.DarkRed);
                    var tableInfoLabel = "Here is a table of failed to map properties".GetBeLabel();
                    operationGrid.AddElement(unsuccessfulLabel);
                    operationGrid.AddElement(tableInfoLabel);

                    var propertiesTable = BeControls.GetGrid(GridOrientation.Horizontal);

                    // left column - input class failed to map properties
                    var inputPropertiesGrid = BeControls.GetGrid();
                    inputPropertiesGrid.AddElement("Input class properties".GetBeLabel().WithCustomTextSize(BeFontSize.BIGGER));
                    foreach (var errorInputProperty in mappingResult.FailedToMapPropertiesContainer.FromClassPropertyNames)
                    {
                        var propertyNameText = BeControls.GetTextControl(errorInputProperty).WithLineBorder(BeShowBorders.All);
                        inputPropertiesGrid.AddElement(propertyNameText);
                    }
                
                    // right column - output class failed to map properties
                    var outputPropertiesGrid = BeControls.GetGrid();
                    outputPropertiesGrid.AddElement("Output class properties".GetBeLabel().WithCustomTextSize(BeFontSize.BIGGER));
                    foreach (var errorOutputProperty in mappingResult.FailedToMapPropertiesContainer.ToClassPropertyNames)
                    {
                        var propertyNameText = BeControls.GetTextControl(errorOutputProperty).WithLineBorder(BeShowBorders.All);
                        outputPropertiesGrid.AddElement(propertyNameText);
                    }

                    propertiesTable.AddElement(inputPropertiesGrid);
                    propertiesTable.AddElement(outputPropertiesGrid);

                    operationGrid.AddElement(propertiesTable);   
                }

                mappingTable.AddElement(operationGrid);
            }

            return mappingTable;
        }
    }
}