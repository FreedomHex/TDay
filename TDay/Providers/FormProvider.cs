using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace TDay
{
    public static class FormProvider
    {
        public static void SerVisulaStyle(DataGridView View)
        {
            foreach (DataGridViewRow Row in View.Rows)
            {
                if (ProfileProvider.GetCategory(int.Parse(Row.Cells["profileIdDataGridViewTextBoxColumn1"].Value.ToString())) != 1)
                {
                    Row.Cells["vanPriceDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                    Row.Cells["roundTripPriceDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                    Row.Cells["bookOfTicketsDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                    Row.Cells["vanPriceDataGridViewTextBoxColumn"].ReadOnly = true;
                    Row.Cells["roundTripPriceDataGridViewTextBoxColumn"].ReadOnly = true;
                    Row.Cells["bookOfTicketsDataGridViewTextBoxColumn"].ReadOnly = true;
                    Row.Cells["profileIdDataGridViewTextBoxColumn1"].ReadOnly = true;
                }
            }
        }

    }
}
