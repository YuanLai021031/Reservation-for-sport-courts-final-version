   M S B A M L   `   `   `  ����.  *Reservation for sport courts final version8    2Reservation_for_sport_courts_final_version.Booking   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Sports Reservation��5   	   $	��450��6$   $	��800��61   .��5      �� ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ #462AD8��6    � Offset$ 0Q�6/   � 5	      $ #DA34AE��6   $
 0.75Q�6/   � 5
      $ #8A16C1��6   $ 1Q�6/   5      ._�5   
   � ��5      �� 5      $	 0,0&�6&   $	 1,1&�67   .��5      � $ #060531��6#   $ 0Q�63   � 5      $ #1B1448��6#   $ 1Q�63   5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��2* �6#   .+�5      �� $��1��6   $��Sports Reservation��6(   
�������$
��Bold"�6U   $��24(�6g   $
��Left=�6u   $��Center=�6�   $��19,0,0,0q�6�   �� 5       $��0��6   $��20,30,20,26q�6)   $��2��6>   .+�5!      �� $��Select sport:��6   
�������$
��Bold"�6D   $��16(�6V   $
��0,10q�6d   �� 5#      -   5         ��Name$ SportComboBox��5#      $
��0,10q�62   .@�5$      �� $��Soccer��6#   �� 5%      $��
Basketball��6#   �� 5&      $��	Badminton��6#   �� 5'      $��Tennis��6#   5(      �� 5*      $��Choose date:��6   
�������$
��Bold"�6C   $��16(�6U   $
��0,10q�6c   (  "System.Windows.Controls.DatePicker  5+      -   5          Name$ BookingDatePicker��5+      $
��0,10q�68   6F   �� 5,      $��Total price:��6   
�������$
��Bold"�6C   $��16(�6U   $
��0,10q�6c   �� 5-      -   5        	 ��Name$	 TotalRriceTextBlock��5-      $
��Bold"�69   $��16(�6K   $
��0,10q�6Y   �� 5.      -   5        
 ��Name$
 
BookButton��5.      $
��Book��6-   $
��left=�6<   $
��0,20q�6W   $	��100��6e   $��23��6�   5/      �� 51      -   5        $	 BookingSummaryTextBlock��51   5   $��1��6   $��1��6%   $��Booking Summary:��6V   
�������$
��Bold"�6�   $��16(�6�   $��20,20,20,20q�6�   $��3��6�   �� 52      $
��Auto=�6   $��1��6>   $��10,49,20,77q�6N   $��1��6c   $	��705��6p   .��53      &   System.Windows.Controls.DataGrid  -   5          Name$ BookingTable��53   2   $y�	Collapsed=�6      AutoGenerateColumns .  "   VerticalScrollBarVisibility$
 Auto=�6d      SelectionUnit$
 Cell=�6�   54      55      �� 56      -   5        $ 	 BookingTotalPriceTextBlock��56      $
��Bold"�6<   $��16(�6N   $��
10,10,0,10q�6\   $��2��6p   $
��Left=�6�   $	��257��6�   57      58      59      