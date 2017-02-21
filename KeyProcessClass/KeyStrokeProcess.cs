using System.Collections.Generic;
using System.Drawing;


namespace CustomTenkey {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class KeyStrokeProcess : IKeyProcess {
        [System.Xml.Serialization.XmlAttribute("pkey")]
        public string PhysicalKey { get; set; }

        [System.Xml.Serialization.XmlAttribute("name")]
        public string Name {
            get {
                return name_text;
            }
            set {
                if (value == null || value.Equals("")) {
                    string substr = (stroke_text.Length > 8) ? stroke_text.Substring(0, 8) : stroke_text;
                    name_text = substr;
                } else {
                    name_text = value;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Color InterfaceColor { get { return Color.AliceBlue; } }

        [System.Xml.Serialization.XmlText]
        public string Stroke {
            get { return stroke_text; }
            set {
                stroke_text = value;
                input = convertKeyList(value);
            }
        }

        [System.NonSerialized]
        [System.Xml.Serialization.XmlIgnore]
        private InputIssuer.INPUT[][] input = null;

        [System.Xml.Serialization.XmlIgnore]
        private static Dictionary<char, byte[]> A2KList = null;

        [System.Xml.Serialization.XmlIgnore]
        private string name_text = null;

        [System.Xml.Serialization.XmlIgnore]
        private string stroke_text = null;

        static KeyStrokeProcess() {
            initA2KList();
        }

        public KeyStrokeProcess() {
        }

        public KeyStrokeProcess(string name, string stroke) {
            if (name == null || name.Equals("")) {
                string substr = (stroke.Length > 8) ? stroke.Substring(0, 8) : stroke;
                name = substr;
            }
            Name = name;
            Stroke = stroke;
        }

        public string GetStrokeString() {
            return Stroke;
        }

        private InputIssuer.INPUT[][] convertKeyList(string stroke) {
            List<InputIssuer.INPUT[]> list = new List<InputIssuer.INPUT[]>();

            foreach (char c in stroke.ToCharArray()) {
                list.Add(InputIssuer.createKeyStroke(A2KList[c]));
            }

            return list.ToArray();
        }

        private static void initA2KList() {
            if (A2KList == null) {
                A2KList = new Dictionary<char, byte[]>();
                A2KList.Add('a', new byte[] { 0x41, });             //'a'{ Keys.A, }
                A2KList.Add('b', new byte[] { 0x42, });             //'b'{ Keys.B, }
                A2KList.Add('c', new byte[] { 0x43, });             //'c'{ Keys.C, }
                A2KList.Add('d', new byte[] { 0x44, });             //'d'{ Keys.D, }
                A2KList.Add('e', new byte[] { 0x45, });             //'e'{ Keys.E, }
                A2KList.Add('f', new byte[] { 0x46, });             //'f'{ Keys.F, }
                A2KList.Add('g', new byte[] { 0x47, });             //'g'{ Keys.G, }
                A2KList.Add('h', new byte[] { 0x48, });             //'h'{ Keys.H, }
                A2KList.Add('i', new byte[] { 0x49, });             //'i'{ Keys.I, }
                A2KList.Add('j', new byte[] { 0x4A, });             //'j'{ Keys.J, }
                A2KList.Add('k', new byte[] { 0x4B, });             //'k'{ Keys.K, }
                A2KList.Add('l', new byte[] { 0x4C, });             //'l'{ Keys.L, }
                A2KList.Add('m', new byte[] { 0x4D, });             //'m'{ Keys.M, }
                A2KList.Add('n', new byte[] { 0x4E, });             //'n'{ Keys.N, }
                A2KList.Add('o', new byte[] { 0x4F, });             //'o'{ Keys.O, }
                A2KList.Add('p', new byte[] { 0x50, });             //'p'{ Keys.P, }
                A2KList.Add('q', new byte[] { 0x51, });             //'q'{ Keys.Q, }
                A2KList.Add('r', new byte[] { 0x52, });             //'r'{ Keys.R, }
                A2KList.Add('s', new byte[] { 0x53, });             //'s'{ Keys.S, }
                A2KList.Add('t', new byte[] { 0x54, });             //'t'{ Keys.T, }
                A2KList.Add('u', new byte[] { 0x55, });             //'u'{ Keys.U, }
                A2KList.Add('v', new byte[] { 0x56, });             //'v'{ Keys.V, }
                A2KList.Add('w', new byte[] { 0x57, });             //'w'{ Keys.W, }
                A2KList.Add('x', new byte[] { 0x58, });             //'x'{ Keys.X, }
                A2KList.Add('y', new byte[] { 0x59, });             //'y'{ Keys.Y, }
                A2KList.Add('z', new byte[] { 0x5A, });             //'z'{ Keys.Z, }

                A2KList.Add('A', new byte[] { 0xA0, 0x41, });       //'A'{ Keys.LShiftKey, Keys.A, }
                A2KList.Add('B', new byte[] { 0xA0, 0x42, });       //'B'{ Keys.LShiftKey, Keys.B, }
                A2KList.Add('C', new byte[] { 0xA0, 0x43, });       //'C'{ Keys.LShiftKey, Keys.C, }
                A2KList.Add('D', new byte[] { 0xA0, 0x44, });       //'D'{ Keys.LShiftKey, Keys.D, }
                A2KList.Add('E', new byte[] { 0xA0, 0x45, });       //'E'{ Keys.LShiftKey, Keys.E, }
                A2KList.Add('F', new byte[] { 0xA0, 0x46, });       //'F'{ Keys.LShiftKey, Keys.F, }
                A2KList.Add('G', new byte[] { 0xA0, 0x47, });       //'G'{ Keys.LShiftKey, Keys.G, }
                A2KList.Add('H', new byte[] { 0xA0, 0x48, });       //'H'{ Keys.LShiftKey, Keys.H, }
                A2KList.Add('I', new byte[] { 0xA0, 0x49, });       //'I'{ Keys.LShiftKey, Keys.I, }
                A2KList.Add('J', new byte[] { 0xA0, 0x4A, });       //'J'{ Keys.LShiftKey, Keys.J, }
                A2KList.Add('K', new byte[] { 0xA0, 0x4B, });       //'K'{ Keys.LShiftKey, Keys.K, }
                A2KList.Add('L', new byte[] { 0xA0, 0x4C, });       //'L'{ Keys.LShiftKey, Keys.L, }
                A2KList.Add('M', new byte[] { 0xA0, 0x4D, });       //'M'{ Keys.LShiftKey, Keys.M, }
                A2KList.Add('N', new byte[] { 0xA0, 0x4E, });       //'N'{ Keys.LShiftKey, Keys.N, }
                A2KList.Add('O', new byte[] { 0xA0, 0x4F, });       //'O'{ Keys.LShiftKey, Keys.O, }
                A2KList.Add('P', new byte[] { 0xA0, 0x50, });       //'P'{ Keys.LShiftKey, Keys.P, }
                A2KList.Add('Q', new byte[] { 0xA0, 0x51, });       //'Q'{ Keys.LShiftKey, Keys.Q, }
                A2KList.Add('R', new byte[] { 0xA0, 0x52, });       //'R'{ Keys.LShiftKey, Keys.R, }
                A2KList.Add('S', new byte[] { 0xA0, 0x53, });       //'S'{ Keys.LShiftKey, Keys.S, }
                A2KList.Add('T', new byte[] { 0xA0, 0x54, });       //'T'{ Keys.LShiftKey, Keys.T, }
                A2KList.Add('U', new byte[] { 0xA0, 0x55, });       //'U'{ Keys.LShiftKey, Keys.U, }
                A2KList.Add('V', new byte[] { 0xA0, 0x56, });       //'V'{ Keys.LShiftKey, Keys.V, }
                A2KList.Add('W', new byte[] { 0xA0, 0x57, });       //'W'{ Keys.LShiftKey, Keys.W, }
                A2KList.Add('X', new byte[] { 0xA0, 0x58, });       //'X'{ Keys.LShiftKey, Keys.X, }
                A2KList.Add('Y', new byte[] { 0xA0, 0x59, });       //'Y'{ Keys.LShiftKey, Keys.Y, }
                A2KList.Add('Z', new byte[] { 0xA0, 0x5A, });       //'Z'{ Keys.LShiftKey, Keys.Z, }

                A2KList.Add('1', new byte[] { 0x31, });             //'1'{ Keys.D1, }
                A2KList.Add('2', new byte[] { 0x32, });             //'2'{ Keys.D2, }
                A2KList.Add('3', new byte[] { 0x33, });             //'3'{ Keys.D3, }
                A2KList.Add('4', new byte[] { 0x34, });             //'4'{ Keys.D4, }
                A2KList.Add('5', new byte[] { 0x35, });             //'5'{ Keys.D5, }
                A2KList.Add('6', new byte[] { 0x36, });             //'6'{ Keys.D6, }
                A2KList.Add('7', new byte[] { 0x37, });             //'7'{ Keys.D7, }
                A2KList.Add('8', new byte[] { 0x38, });             //'8'{ Keys.D8, }
                A2KList.Add('9', new byte[] { 0x39, });             //'9'{ Keys.D9, }
                A2KList.Add('0', new byte[] { 0x30, });             //'0'{ Keys.D0, }

                A2KList.Add('-', new byte[] { 0xBD, });             //'-'{ Keys.OemMinus, }
                A2KList.Add('=', new byte[] { 0xBB, });             //'='{ Keys.Oemplus, }
                A2KList.Add('[', new byte[] { 0xDB, });             //'['{ Keys.Oem4, }
                A2KList.Add(']', new byte[] { 0xDD, });             //']'{ Keys.Oem6, }
                A2KList.Add(';', new byte[] { 0xBA, });             //';'{ Keys.Oem1, }
                A2KList.Add(',', new byte[] { 0xBC, });             //','{ Keys.Oemcomma, }
                A2KList.Add('.', new byte[] { 0xBE, });             //'.'{ Keys.OemPeriod, }
                A2KList.Add('/', new byte[] { 0xBF, });             //'/'{ Keys.Oem2, }
                A2KList.Add('`', new byte[] { 0xC0, });             //'`'{ Keys.Oem3, }

                A2KList.Add('!', new byte[] { 0xA0, 0x31, });       //'!'{ Keys.LShiftKey, Keys.D1, }
                A2KList.Add('@', new byte[] { 0xA0, 0x32, });       //'@'{ Keys.LShiftKey, Keys.D2, }
                A2KList.Add('#', new byte[] { 0xA0, 0x33, });       //'#'{ Keys.LShiftKey, Keys.D3, }
                A2KList.Add('$', new byte[] { 0xA0, 0x34, });       //'$'{ Keys.LShiftKey, Keys.D4, }
                A2KList.Add('%', new byte[] { 0xA0, 0x35, });       //'%'{ Keys.LShiftKey, Keys.D5, }
                A2KList.Add('^', new byte[] { 0xA0, 0x36, });       //'^'{ Keys.LShiftKey, Keys.D6, }
                A2KList.Add('&', new byte[] { 0xA0, 0x37, });       //'&'{ Keys.LShiftKey, Keys.D7, }
                A2KList.Add('*', new byte[] { 0xA0, 0x38, });       //'*'{ Keys.LShiftKey, Keys.D8, }
                A2KList.Add('(', new byte[] { 0xA0, 0x39, });       //'('{ Keys.LShiftKey, Keys.D9, }
                A2KList.Add(')', new byte[] { 0xA0, 0x30, });       //')'{ Keys.LShiftKey, Keys.D0, }

                A2KList.Add('_', new byte[] { 0xA0, 0xBD, });       //'_'{ Keys.LShiftKey, Keys.OemMinus, }
                A2KList.Add('+', new byte[] { 0xA0, 0xBB, });       //'+'{ Keys.LShiftKey, Keys.Oemplus, }
                A2KList.Add('{', new byte[] { 0xA0, 0xDB, });       //'{'{ Keys.LShiftKey, Keys.Oem4, }
                A2KList.Add('}', new byte[] { 0xA0, 0xDD, });       //'}'{ Keys.LShiftKey, Keys.Oem6, }
                A2KList.Add(':', new byte[] { 0xA0, 0xBA, });       //':'{ Keys.LShiftKey, Keys.Oem1, }
                A2KList.Add('<', new byte[] { 0xA0, 0xBC, });       //'<'{ Keys.LShiftKey, Keys.Oemcomma, }
                A2KList.Add('>', new byte[] { 0xA0, 0xBE, });       //'>'{ Keys.LShiftKey, Keys.OemPeriod, }
                A2KList.Add('?', new byte[] { 0xA0, 0xBF, });       //'?'{ Keys.LShiftKey, Keys.Oem2, }
                A2KList.Add('~', new byte[] { 0xA0, 0xC0, });       //'~'{ Keys.LShiftKey, Keys.Oem3, }
                A2KList.Add('|', new byte[] { 0xA0, 0xDC, });       //'|'{ Keys.LShiftKey, Keys.Oem5, }

                A2KList.Add('\t', new byte[] { 0x09, });            //'\t { Keys.Tab, }
                A2KList.Add('\n', new byte[] { 0x0D, });            //'\n { Keys.Enter, }
                A2KList.Add('\r', new byte[] { 0x0A, });            //'\n { Keys.Enter, }
                A2KList.Add('\'', new byte[] { 0xDE, });            //'\' { Keys.Oem7, }
                A2KList.Add('\"', new byte[] { 0xA0, 0xDE, });      //'\" { Keys.LShiftKey, Keys.Oem7, }
                A2KList.Add('\\', new byte[] { 0xDC, });            //'\\ { Keys.Oem5, }
                A2KList.Add('\b', new byte[] { 0x08, });            //'\b { Keys.Back, }
                A2KList.Add(' ', new byte[] { 0x20, });             //' '{ Keys.Space, }
            }
        }

        public void Leave() {
        }

        public void Press() {
            foreach (InputIssuer.INPUT[] i in input) {
                InputIssuer.sendInput(i);
            }
        }
    }
}
