   M S B A M L   `   `   `  ����I  EWpfEfSampleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfEfSampleAppWpfEfSampleApp  5   	   $    WpfEfSampleApp.AuctionItemView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,srcclr-namespace:WpfEfSampleApp     5   	     N� WindowWidth$	  600Q�5       N� WindowHeight$	 650Q�6    N� Title$ AuctionItemView��5   	   $	��650��6!   $	��550��6.    � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property	 �� Value   groupingHeaderTemplate
 �� Path  checkBoxStyle  listingDataView �� Source �� Current �� Source��%�  '��       &	  �     &	     &	 w    �� 5	   
   " ��6    5
      P� 5       �@��
IsSelected . . �5      ��  � ��
	 �����63   5      5      �� 5   
   .R�5      �� 
����  �$
��Bold"�5   *   $��12(�6<   ��5      �� $

 Name�5   <   5      �� 5   
   " ��6&   .�5      ��  � ��	 �#3333336+   �� 5       � ��$
	 Bold"�6+   5      �� 5       5      �� # Z $
 AuctionItems�5      5      .��5      � -   5         � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $	��400 �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6    � ColumnDefinitions 5$   
   �� 5%      �� 5&      �� 5'      .+�5)   
   �� $��0��6   $��2��6!     
titleStyle#��[ 5*      $��8,20,8,8q�68   List of items for sale:6d   �� 5+   
   -   5         ��Name$
 Menu��5,       �� Orientation$ 
Horizontal=�5+      $��Right=�6.   $��0��6J   $��1��6W   $��2��6g   
������ .+�5-      �� -   5          ButtonStyle#���  5-      $��Right=�6;   $��10q�6W   .��5.      �� $��10��6   $��10��6#     	CloseIcon#���  6/   5/      50      �� 51   
   -   5         ��Name$ CurrentUserItem��52   Y   $
��Fill=�6   $��0��6'   $��2��64   $��
64,10,36,0q�6E   ��51      �� $
 	UserImage�  ImageToSourceConverter �� 	Converter# [ 52   Y   �� 53   
   -   5        $��Grouping��53      $��1��6#   $��0��60   $��8q�54      #��[ 6   .��6E   Grouping by category6[   �� 55   
   -   5        $��	Filtering��55      $��1��6$   $��1��61   $��8q�56      #��[ 6   .��6E   Show only bargains6Y   �� 57   
   -   5        $��Sorting��57      $��1��6"   $��3��6/   $��8q�58      #��[ 6   .��6E   Sort by category and date6`   �� 59   
   -   5         ��Name$ Master��59      $��2��6"   $��8q�6/   $��3��6:   $ �� IsSynchronizedWithCurrentItem$
 TrueY�5:      ��5;      �� # [  �� 
GroupStyle 5?      (  "System.Windows.Controls.GroupStyle  5@         HeaderTemplate# [  6   5B      �� 5C   
   -	   5        $��Detail��5C      $��3��6'   $��3��64    ! detailsProductListingTemplate#��[ 5E      $��9,0,10,0q�5F      ��5D      �� # [ 5F      �� 5G   
   -
   5        $��OpenAddProduct��5G      $��3��6'   $��2��64   $��Add Product��6D   $��20��6Z   $��Bottom=�6f   $��Right=�5H      $��8q�6-   68   5I      5J      