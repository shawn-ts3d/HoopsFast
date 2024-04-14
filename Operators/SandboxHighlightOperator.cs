using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPS;

namespace HoopsFast
{
    class SandboxHighlightOperator : SelectOperator
    {
        private MainWindow Window { get; set; }

        public SandboxHighlightOperator(MainWindow window) : base(MouseButtons.ButtonLeft(), new ModifierKeys())
        {
            Window = window;
        }

        public override string GetName()
        {
            return "WPF_SandboxHighlightOperator";
        }

        public override bool OnMouseDown(MouseState in_state)
        {
            if (IsMouseTriggered(in_state) && base.OnMouseDown(in_state))
            {
                HighlightCommon();
                return true;
            }

            return false;
        }

        public override bool OnTouchDown(TouchState in_state)
        {
            if (base.OnTouchDown(in_state))
            {
                HighlightCommon();
                return true;
            }

            return false;
        }

        private void HighlightCommon()
        {
            Window.Unhighlight();

            SelectionResults selectionResults = GetActiveSelection();
            if (selectionResults.GetCount() > 0)
            {
                var highlightOptions = new HighlightOptionsKit("highlight_style");
                //if (Window.CADModel != null)
                //{
                //    // make sure the higlight interacts well with Show / Hide / Isolate
                //    highlightOptions.SetOverlay(Drawing.Overlay.InPlace);

                //    // since we have a CADModel, we want to highlight the components, not just the Visualize geometry
                //    SelectionResultsIterator it = selectionResults.GetIterator();
                //    Canvas canvas = Window.GetSprocketsControl().Canvas;
                //    while (it.IsValid())
                //    {
                //        var componentPath = Window.CADModel.GetComponentPath(it.GetItem());
                //        if (!componentPath.Empty())
                //        {
                //            // Make the selected component get highlighted in the model browser
                //            highlightOptions.SetNotification(true);
                //            componentPath.Highlight(canvas, highlightOptions);

                //            // if we selected PMI, highlight the associated components (if any)
                //            Component leafComponent = componentPath.GetComponents()[0];
                //            if (leafComponent.HasComponentType(Component.ComponentType.ExchangePMIMask))
                //            {
                //                // Only highlight the Visualize geometry for the associated components, don't highlight the associated components in the model browser
                //                highlightOptions.SetNotification(false);
                //                foreach (var reference in leafComponent.GetReferences())
                //                    new ComponentPath(new Component[1] { reference }).Highlight(canvas, highlightOptions);
                //            }
                //        }
                //        it.Next();
                //    }
                //}
                //else
                //{
                    // since there is no CADModel, just highlight the Visualize geometry
                    Window.GetSprocketsControl().Canvas.GetWindowKey().GetHighlightControl().Highlight(selectionResults, highlightOptions);
                    Database.GetEventDispatcher().InjectEvent(new HighlightEvent(HighlightEvent.Action.Highlight, selectionResults, highlightOptions));
                //}
            }

            Window.Update();
        }
    }
}
