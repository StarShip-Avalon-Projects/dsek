using System;
using System.Drawing;
using System.Collections.Generic;

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
			Number,
			StringVar,
			NumVar,
			Header
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
				case STYLE_HEADER:
					CurrentState = State.Header;
					break;
                case STYLE_NUM_VAR:
                    CurrentState = State.NumVar;
                    break;
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
                                 CurrentState = State.Number;
                                 Consume();
                                 SetStyle(STYLE_NUMBER);
                                    break;
							case '{':
								CurrentState = State.String;
								break;
							case '*':
								CurrentState = State.Comment;

								break;
							case '%':
								CurrentState = State.NumVar;
								//Consume();
								//SetStyle(STYLE_NUM_VAR);
								//consumed = true;
								break;
							case '~':
								CurrentState = State.StringVar;
								//Consume();
								//SetStyle(STYLE_STR_VAR);
								//consumed = true;
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
						ConsumeString(STYLE_STRING, '\0',false, false, '}');
						consumed = true;
						CurrentState = State.Unknown;
						break;
					case State.NumVar:
						if (!IsIdentifier(CurrentCharacter))
						{
							CurrentState = State.Unknown;
							SetStyle(STYLE_DEFAULT);
						}
						else
						{
                            Consume();
							SetStyle(STYLE_NUM_VAR); 
							
						}

			  
						break;
					case State.StringVar:
						if (!IsIdentifier(CurrentCharacter))
						{
							CurrentState = State.Unknown;
							SetStyle(STYLE_DEFAULT);
						}
						else
						{
							SetStyle(STYLE_STR_VAR);
							Consume();
						}
						
					   
						break;
                    case State.Number:
                          if (IsNum(CurrentCharacter))
                          {
                              Consume(); 
                                SetStyle(STYLE_NUMBER);
                        
                            }
                          else
                          {
                              CurrentState = State.Unknown;
                              SetStyle(STYLE_DEFAULT);
                          }
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
                case State.NumVar:
                    SetStyle(STYLE_NUM_VAR);
                    break;
				case State.String:
					SetStyle(STYLE_STRING);
					StyleNextLine(); // Continue the style to the next line
					break;
                case State.Number:
                       SetStyle(STYLE_NUMBER);
                      Consume();
                       break;
                default:
					throw new Exception("Unknown state!");
			}
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
					return true;
				default:
					return false;
			}
		}

		
	}
}