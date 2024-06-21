using DragonRunes.Network;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Client.Scripts.SceneScript.MainMenu.Windows
{
    public partial class winNewChar : WindowBase
    {
        public override void _Ready()
        {
            base._Ready();

            // Inicia os componentes da cena
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            AssignButtons();
            AssignTextFields();

        }
        private void AssignButtons()
        {
            NodeManager.GetNode<Button>("btnCOk").Pressed += () => CreateNewChar();
            //NodeManager.GetNode<Button>("optGender").Connect("item_selected", new Callable(this, nameof(GenderOption)));
        }
        private void AssignTextFields()
        {
            NodeManager.GetNode<LineEdit>("txtCharName").TextChanged += (text) => CharNameText(text);
        }

        private void CharNameText(string text)
        {
            if (text.IsValidName())
                NodeManager.GetNode<LineEdit>("txtCharName").AddThemeColorOverride("font_color", new Color(0, 1, 0));
            else
                NodeManager.GetNode<LineEdit>("txtCharName").AddThemeColorOverride("font_color", new Color(1, 0, 0));
        }

        private void CreateNewChar()
        {
            var nameField = NodeManager.GetNode<LineEdit>("txtCharName").Text;
            var genderField = NodeManager.GetNode<OptionButton>("optGender").GetSelectedId();

            if (nameField.IsValidName() && genderField > (byte)Gender.None && genderField < Enum.GetValues(typeof(Gender)).Length )
            {
                var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));

                var serverPeer = clientManager._player.CurrentPeer;

                var packetProcessor = clientManager._networkService._clientPacketProcessor;

                packetProcessor.SendNewChar(serverPeer, nameField, (Gender)genderField);
            }
        }
    }
}
