   M S B A M L   `   `   `  ����F  BFileUpdater, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FileUpdater.ViewFileUpdater.View  5   	       FileUpdater.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3controlsclr-namespace:FileUpdater.View     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��800��6)   $	��470��65   $	��110��6D   .��5      �  � ColumnDefinitions 5   
   �� 5      $��5* �6   $	��219��6*   �� 5	      $��5* �6   $	��235��6*    � RowDefinitions 5   
   �� 5      �� 5      $��40 �6   .+�5   
   ��  �� ResizeDirection$ Columns=�6   $��4��61   $��2��6;   �� 5   
   -   5        $��SourceStack��5      $��0��6)   $��0,0,4,0q�69   �� 5   
   -   5        $��	DestStack��5      $��1��6&   �� 5   
   -   5        $��AddSourceBtn��5      $��0��6%   $��1��65   $��
Add Source��6B   $	��100��6W   $��30��6c   $
��Left=�6o   $��5,0,0,0q�6�   �� 5   
   -   5        $��RemoveSourceBtn��5      $��0��6(   $��1��68   $��Remove Source��6E   $	��100��6]   $��30��6i   $
��Left=�6u   $��	110,0,0,0q�6�   �� 5   
   -   5        $��
AddDestBtn��5      $��1��6#   $��1��63   $��Add Destiantion��6@   $	��110��6Z   $��30��6f   $
��Left=�6r   $��5,0,0,0q�6�   �� 5   
   -   5        $��RemoveDestBtn��5      $��1��6&   $��1��66   $��Remove Destination��6C   $	��120��6`   $��30��6l   $
��Left=�6x   $��	120,0,0,0q�6�   5      5      