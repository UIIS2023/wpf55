   M S B A M L   `   `   `  ����H  DPrikazFilmova, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PrikazFilmovaPrikazFilmova  5   	       PrikazFilmova.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PrikazFilmova     5   	     � Title$  Projekcija Filmova��5   	   $	��350��6$   $	��550��61   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridCentralni��5
      $��Center=�6.   $��Center=�6K   $
��auto��6f   $
��auto��6t      
IsReadOnly .    SelectionUnit$ FullRow=�6�      SelectionMode$ Single=�6�      CanUserAddRows .     AlternatingRowBackground
 ��� �6�   �� 5   
   $��Center=�6   $	��Top=�62   $��30��6J   $
��auto��6V    �� Orientation$ 
Horizontal=�6c   .+�5      �� -   5         ��Name$ 
btnFilmovi��5      $��Filmovi��6)   $
��Left=�6;   $	��Top=�6V   $��30��6n   $��75��6z   6�   �� 5      -   5        $ 
btnZanrovi��5      $��Zanrovi��6)   $
��Left=�6;   $	��Top=�6V   $��30��6n   $��75��6z   6�   �� 5      -   5        $ btnReziseri��5      $��Reziseri��6*   $
��Left=�6=   $	��Top=�6X   $��30��6p   $��75��6|   6�   �� 5      -   5        $ 	btnGlumci��5      $��Glumci��6(   $
��Left=�69   $	��Top=�6T   $��30��6l   $��75��6x   6�   �� 5      -   5        $ btnKarta��5      $��Karta��6'   $
��Left=�67   $	��Top=�6R   $��30��6j   $��75��6v   6�   5      �� 5   
   $��Center=�6   $��Bottom=�62   $��30��6M   $
��auto��6Y   $ 
Horizontal=�6f   .+�5      �� -   5        $ btnDodaj��5      $��Dodaj��6'   $	��120��67   6C   �� 5      -   5        $ 	btnIzmeni��5      $��Izmeni��6(   $	��120��69   6E   �� 5      -	   5        $ 	btnObrisi��5      $��Obrisi��6(   $	��120��69   6E   5      5      5      