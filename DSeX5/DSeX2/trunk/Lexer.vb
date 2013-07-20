using System;
using System.Drawing;
using System.Collections.Generic;

namespace ScintillaNET.Lexers
{
    public sealed class CtlLexer : CustomLexer
    {
        public override string LexerName { get { return "ctl"; } }


        private const int STYLE_STRING = 11;
        private const int STYLE_NUMBER = 12;
        private const int STYLE_COMMENT = 14;
        private const int STYLE_OPERATOR = 15;

        public CtlLexer(Scintilla scintilla) : base(scintilla) { }

        private enum State : int
        {
            Unknown = STATE_UNKNOWN,
            String,
            Comment,
        }

        new private State CurrentState
        {
            get { return (State)base.CurrentState; }
            set { base.CurrentState = (int)value; }
        }

        protected override void InitializeStateFromStyle(int style)
        {
            switch (style)
            {
                case STYLE_STRING:
                    CurrentState = State.String;
                    break;
                // Otherwise we don't need to carry the
                // state on from the previous line.
                default:
                    break;
            }
        }

        protected override void Style()
        {
            StartStyling();

            while (!EndOfText)
            {
                switch (CurrentState)
                {
                    case State.Unknown:
                        bool consumed = false;
                        switch (CurrentCharacter)
                        {

                            case '\'':
                                CurrentState = State.String;
                                break;
                            case '=':
                            case '>':
                            case '<':
                            case '!':
                            case '(':
                            case ')':
                            case '[':
                            case ']':
                            case ';':
                                Consume();
                                SetStyle(STYLE_OPERATOR);
                                consumed = true;
                                break;
                            case '#':
                                CurrentState = State.Comment;
                                break;
                            default:
                                if (IsWhitespace(CurrentCharacter))
                                {
                                    ConsumeWhitespace();
                                    consumed = true;
                                }
                                else
                                {
                                    SetStyle(STYLE_DEFAULT);
                                }
                                break;
                        }
                        if (!consumed)
                            Consume();
                        break;
                    case State.Comment:
                        ConsumeUntilEOL(STYLE_COMMENT);
                        CurrentState = State.Unknown;
                        break;
                    case State.String:
                        // We're using '\0' as the escape character because a NUL character
                        // doesn't usually appear in text-based documents.
                        ConsumeString(STYLE_STRING, '\0', false, false, '\'');
                        consumed = true;
                        CurrentState = State.Unknown;
                        break;
                    default:
                        throw new Exception("Unknown state!");
                }
            }

            switch (CurrentState)
            {
                case State.Unknown: break;
                case State.Comment:
                    SetStyle(STYLE_COMMENT);
                    break;
                case State.String:
                    SetStyle(STYLE_STRING);
                    StyleNextLine(); // Continue the style to the next line
                    break;
                default:
                    throw new Exception("Unknown state!");
            }
        }
    }
}