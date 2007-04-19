using System;

using MonoDevelop.Projects;
using MonoDevelop.Core;

using Gtk;

namespace MonoDevelop.Autotools
{
	public class TarballTargetEditorWidget : VBox
	{
		public TarballTargetEditorWidget (TarballDeployTarget target, Combine targetCombine)
		{
			HBox dir_entry = new HBox ();
			
			Label lab = new Label ( GettextCatalog.GetString ("Deploy directory:") );
			dir_entry.PackStart (lab, false, false, 0);
			
			Gnome.FileEntry fe = new Gnome.FileEntry ("tarball-folders","Target Directory");
			fe.GtkEntry.Text = target.TargetDir;
			fe.Directory = true;
			fe.Modal = true;
			fe.UseFilechooser = true;
			fe.FilechooserAction = FileChooserAction.SelectFolder;
			fe.GtkEntry.Changed += delegate (object s, EventArgs args) {
				target.TargetDir = fe.GtkEntry.Text;
			};
			dir_entry.PackStart (fe, true, true, 6);

			PackStart ( dir_entry , false, false, 0 );
			
			HBox config_box = new HBox ();

			Label conlab = new Label ( GettextCatalog.GetString ("Default configuration:") );
			config_box.PackStart (conlab, false, false, 0);
			
			if ((target.DefaultConfiguration == null || target.DefaultConfiguration == "") && targetCombine.ActiveConfiguration != null)
				target.DefaultConfiguration = targetCombine.ActiveConfiguration.Name;
			
			ComboBox configs = ComboBox.NewText ();
			for ( int ii=0; ii < targetCombine.Configurations.Count; ii++ )
			{
				string cc = targetCombine.Configurations [ii].Name;
				configs.AppendText ( cc );
				if ( cc == target.DefaultConfiguration ) configs.Active = ii;
			}
			configs.Changed += delegate (object s, EventArgs args) {
				target.DefaultConfiguration = targetCombine.Configurations [configs.Active].Name;
			};
			config_box.PackStart ( configs, true, true, 6 );

			PackStart ( config_box, false, false, 6 );

			ShowAll ();
		}
	}
}
