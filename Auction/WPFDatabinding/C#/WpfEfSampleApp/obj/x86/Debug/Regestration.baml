   M S B A M L   `   `   `  ����I  EWpfEfSampleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfEfSampleAppWpfEfSampleApp  5      !    WpfEfSampleApp.Regestration   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,srcclr-namespace:WpfEfSampleApp     5      -   5          N� ShowsNavigationUI  .  N� WindowWidth$	 450Q�5       N� WindowHeight$	 450Q�6    N� Title$ Regestration��5
       � 	Resources 5      +   %WpfEfSampleApp.ByteArrayToImageSource   ImageToSourceConverter��%   &	          5   
   6%   5      .��5      �� ._�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5        $��2q�5      $��0��6*   ._�5      �   ConversationBackground#���  6    5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $��2 �6(   �� 5      $
��Auto �6(   �� 5      $��2 �6(   �� 5      $
��Auto �6(   �� 5      $��10 �6(   �� 5       �� 5!      $
��Auto �6(    � ColumnDefinitions 5#      �� 5$      $	��106 �6+   �� 5%      $��* �6+   .+�5'      �� -   5        $��0��5'      $��0��6+     ButtonStyle#���  6;   $��Right=�6a   $��10q�6}   .��5(      �� $��10��6    $��10��6+     	CloseIcon#���  67   5)      �� 5*      $��0��6    $��1��6-    �� TextAlignment$ Right=�6>     
titleStyle#��[ 5+       $��5q�5,       Regestration User Item6C   �� 5-      $��1��6    $��0��6-     smallTitleStyle#��[ 6=   $��0,5,0,5q�6f   Name:6   �� 5.      $��8,0,8,0q�6   $��1��6.   $��1��6;     WindowBorder#���  6K   $��2q�6x    �� CornerRadius$ 8��6�   
�������._�5/      �� -   5        $��UserNameEntryForm��5/   "   $��4,2,2,2q�6=   $
��Bold"�6O   
������ 
������ $��0q�6�   ��50      �� 	 �� Path$
	 Name�
 �� UpdateSourceTrigger$
 PropertyChanged=�51      �� 52      -   5        $��UserPassEntyForm��52       $��3��68   $��0��6E   #��[ 6U   $��0,5,0,5q�6~   	Password:6�   �� 53      $��8,0,8,0q�6   $��3��6.   $��1��6;   #���  6K   $��2q�6x   $ 8��6�   
�������._�54      G� -   5         G�Name$ UserPassword��54   &   
������ 
������ $��0q�6o   $��4,2,2,2q�6�   $��Hidden=�6�   6�   55      �� 56      $��5��6    $��0��6-   #��[ 6=   $��0,5,0,5q�6f   Email:6   �� 57      $��8,0,8,0q�6   $��5��6.   $��1��6;   #���  6K   $��2q�6x   $ 8��6�   
�������._�58      �� -   5        $��UserEmailEntryForm��58   "   $��4,2,2,2q�6>   $
��Bold"�6P   
������ 
������ $��0q�6�   ��59      �� $	 Email�$
 PropertyChanged=�5:      �� 5;      $��7��6!   $��0��6.   .+�5<      �� #��[ 6%   $��0,5,0,5q�6N   Photo:6g   �� 5=      -   5         ��Name$ ConnectButton��5=   !   $��2q�68   #���  6L   #���  6y   $��Right=�6�   $��20,5,8,5q�5>      $��6��6,   $��0,0,5,10q�67   $	��Top=�6I   6a   5@      �� 5A      $��8,0,8,0q�6   $��7��6.   $��1��6;   #���  6L   $��2q�6y   $ 20��6�   
������ ._�5B      �� $
��Fill=�6v   ��6    �� $	 	UserImage� �� 	Converter# [  6v   5C      �� 5D      $��8��6    $��2��6-   $��Center=�6A   $��Right=�6\   $��0 0 20 0q�6x   �� 5E      -	   5        
����� �.�5E   P   Save6W   5F      5H      5I      5J      5L      5O      