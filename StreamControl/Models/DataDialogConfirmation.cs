using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;

namespace StreamControl
{
    public class DataDialogConfirmation<TData> : Confirmation
    {
        public TData Data { get; set; }

        public DataDialogConfirmation(TData data) : base()
        {
            Data = data;
        }
    }
}