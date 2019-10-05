namespace Server.Gumps
{
  public delegate void WarningGumpCallback( bool okay );

  public class WarningGump : Gump
  {
    private WarningGumpCallback m_Callback;

    public WarningGump( int header, int headerColor, object content, int contentColor, int width, int height, WarningGumpCallback callback = null, bool cancelButton = true) : base( (640 - width) / 2, (480 - height) / 2 )
    {
      m_Callback = callback;

      Closable = false;

      AddPage( 0 );

      AddBackground( 0, 0, width, height, 5054 );

      AddImageTiled( 10, 10, width - 20, 20, 2624 );
      AddAlphaRegion( 10, 10, width - 20, 20 );
      AddHtmlLocalized( 10, 10, width - 20, 20, header, headerColor );

      AddImageTiled( 10, 40, width - 20, height - 80, 2624 );
      AddAlphaRegion( 10, 40, width - 20, height - 80 );

      if ( content is int i )
        AddHtmlLocalized( 10, 40, width - 20, height - 80, i, contentColor, false, true );
      else if ( content is string )
        AddHtml( 10, 40, width - 20, height - 80, $"<BASEFONT COLOR=#{contentColor:X6}>{content}</BASEFONT>", false, true );

      AddImageTiled( 10, height - 30, width - 20, 20, 2624 );
      AddAlphaRegion( 10, height - 30, width - 20, 20 );

      AddButton( 10, height - 30, 4005, 4007, 1 );
      AddHtmlLocalized( 40, height - 30, 170, 20, 1011036, 32767 ); // OKAY

      if ( cancelButton )
      {
        AddButton( 10 + ((width - 20) / 2), height - 30, 4005, 4007, 0 );
        AddHtmlLocalized( 40 + ((width - 20) / 2), height - 30, 170, 20, 1011012, 32767 ); // CANCEL
      }
    }

    public override void OnResponse( Network.NetState sender, RelayInfo info )
    {
      if (m_Callback == null)
        return;

      if ( info.ButtonID == 1)
        m_Callback( true );
      else
        m_Callback.Invoke( false );
    }
  }
}
