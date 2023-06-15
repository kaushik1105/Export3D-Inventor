using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Inventor;

namespace InvAddIn.Model
{
    public interface IInventorHandler
    {
        void Export3D();
    }
 

    public class InventorHandler : IInventorHandler
    {
        private Inventor.Application invInstance;


        public InventorHandler(Inventor.Application _instance)
        {
            invInstance = _instance;

        }

        /// <summary>
        /// Performs the Export3D
        /// </summary>
        public void Export3D()
        {
            Inventor.AssemblyDocument AsmDoc = invInstance.ActiveDocument as Inventor.AssemblyDocument;
            if (AsmDoc != null)
            {
                MessageBox.Show("This is an assembly");
            }
            else
            {
                Inventor.PartDocument PartDoc = invInstance.ActiveDocument as Inventor.PartDocument;
                if (PartDoc != null)
                {
                    MessageBox.Show("This is a part");
                }
                else
                {
                    MessageBox.Show("This is something else");
                }
            }


        }

    }
                 
    
}
