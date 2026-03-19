using System;
using Karianakis.Utilities;
using UnityEngine;

namespace Karianakis.EditTools
{
    public partial class DynamicDebugItem
    {


        public DynamicDebugItem SetNickname(string nickname)
        {
            _nickname = nickname;
            return this;
        }
        public DynamicDebugItem SetContent(string c)
        {
            _content = c;
            Invo.Simple(Refresh, 0f);
            return this;
        }
        public DynamicDebugItem SetColor(Color c)
        {
            _color = c;
            Invo.Simple(Refresh, 0f);
            return this;
        }
        public DynamicDebugItem SetDynamicEnabled(Func<bool> c)
        {
            _GetEnabled = c;
            return this;
        }
        public DynamicDebugItem SetDynamicColor(Func<Color> c)
        {
            _Getcollor = c;
            return this;
        }
        public DynamicDebugItem SetDynamicContent(Func<string> c)
        {
            _GetContent = c;
            return this;
        }

        public DynamicDebugItem SetUpdate(float c)
        {
            _interval = c;

            if (_id != null)
            {
                InvoManager.KillAll(_id);
            }
            else
            {
                _id = new MyId();
            }

            Invo.Infinite(Refresh, c)
            .SetStartDelay(0.05f)
            .SetId(_id);

            return this;
        }




        //! OUT FOR NOW
        DynamicDebugItem PinTop()
        {
            // pin to top of items
            _pinBottom = false;
            _pinTop = true;
            return this;

        }
        DynamicDebugItem PinBottom()
        {
            // pin to bottom of items
            _pinTop = false;
            _pinBottom = true;
            return this;
        }

        DynamicDebugItem ForcePanelOpen()
        {
            DynamicDebugManager.ForceVissibility(true);
            return this;
        }

        //Todo
        //? MAKE






        //? IDEAS
        DynamicDebugItem CloseAfter__idea()
        {
            // will close after but why ??
            return this;
        }
        DynamicDebugItem KillAfter__idea()
        {
            // will kill after but why ??
            // kill meaning completely out ..
            return this;
        }






    }
}
