   M S B A M L   `   `   `  ����I  EWpfEfSampleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfEfSampleAppWpfEfSampleApp  5          WpfEfSampleApp.ActualBid   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,srcclr-namespace:WpfEfSampleApp     5      -   5        $	��650��5      $	��750��6     N� Title$  	ActualBid��5       � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value   listingPostsView �� Source	 �� Path��%�   '��       &	  �     �� 5   
   " ��6    5	      P� 5
       �@��
IsSelected . . �5      ��  � ��
 �����63   5      5      �� 5   
    5      �� $	 PostsUItems�5      5      .��5      � 
 � RowDefinitions
 5   
   �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      �� 5      $	��200 �6   .+�5   
   �� -   5         ��Name$ MasterPosts��5      $��0��6'   $��0��64   $��8q�5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6   ��5       ��  �� Source# [  �� 5"   
   -   5        $��DetailBidItem��5"      $��0��6.   $��1��6;   $��2��6K    % !delailsAuctionItemListingTemplate#��[ 5#      $��9,0,10,0q�5$      5'      5(      