using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScintillaNET.Lexers
{
	public sealed class dsLexer : CustomLexer
	{
		public override string LexerName { get { return "dragonspeak"; } }
        
		private const int STYLE_STRING = 11;
		private const int STYLE_NUMBER = 12;
		private const int STYLE_COMMENT = 14;
		private const int STYLE_NUM_VAR = 15;
		private const int STYLE_STR_VAR = 16;
		private const int STYLE_HEADER = 17;

		public dsLexer(Scintilla scintilla) : base(scintilla) { }

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

		public override Dictionary<string, int> StyleNameMapping
		{
			get
			{
				return new Dictionary<string, int>()
				{
					{ "Comment", STYLE_COMMENT },
					{ "Number", STYLE_NUMBER },
					{ "String", STYLE_STRING },
					{ "StringVariable", STYLE_STR_VAR},
					{ "NumberVariable", STYLE_NUM_VAR},
					{ "Header", STYLE_HEADER}
				};
			}
		}
		public override Dictionary<string, int> KeywordNameMapping
		{
			get
			{
                return new Dictionary<string, int>();
			}
		}
     
        protected override void Initialize()
        {

            base.Initialize();
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
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                            case '#':
                            case '-':
                                 ConsumeNum(STYLE_NUMBER);
                                 CurrentState = State.Unknown;
                                 consumed = true;
                                 break;
							case '{':
								CurrentState = State.String;
								break;
							case '*':
								CurrentState = State.Comment;

								break;
							case '%':
                                Consume();
                                ConsumeVariable(STYLE_NUM_VAR);
                                CurrentState = State.Unknown;
                                consumed = true;
								break;
							case '~':
                                Consume();
                                ConsumeVariable(STYLE_STR_VAR);
                                consumed = true;
                                CurrentState = State.Unknown;
								break;
							default:
								if (IsWhitespace(CurrentCharacter))
								{
									ConsumeWhitespace();
                                    consumed = true;
								}
								else
								{
                                    Consume(); //This fixes the off-by-one issue and allows the style to be set right.
									SetStyle(STYLE_DEFAULT);
                                    consumed = true;
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
						ConsumeString(STYLE_STRING, '\0',false, false, '}');
						consumed = true;
						CurrentState = State.Unknown;
						break;
				

					default:
						throw new Exception("Unknown state!");
				}
			}

			switch (CurrentState)
			{
				case State.Unknown:
              	case State.Comment:
                     break;
				case State.String:
					SetStyle(STYLE_STRING);
					StyleNextLine(); // Continue the style to the next line
					break;
                default:
					throw new Exception("Unknown state!");
			}
		}

        void ConsumeVariable(int style)
        {
            while (!EndOfText)
            {
                if (!IsIdentifier(CurrentCharacter))
                {
                    Consume();
                    SetStyle(style);
                    return;
                }
                else
                    Consume();
            }
        }
        void ConsumeNum(int style)
        {

            while (!EndOfText)
            {

                    if (IsNum(CurrentCharacter))
                    {
                       
                        Consume();
                      
                    }
                    else 
                        break;
            }

            SetStyle(style);
        }
	 bool IsNum(char c)
		{
			switch (c)
			{
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case '#':
                case '-':
					return true;
				default:
					return false;
			}
		}

		
	}
}