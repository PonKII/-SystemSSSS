   M S B A M L   `   `   `  ����R  NSystem.CourseManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:System.CourseManagement.ViewSystem.CourseManagement.View  5      H DMyControllers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MyControllers;assembly=MyControllersMyControllers 5      1    +System.CourseManagement.View.CoursePageView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:System.CourseManagement.View     5      I
sscontrols2clr-namespace:MyControllers;assembly=MyControllers     5        � 	Resources  5
       �� 
TargetType   CategoryItemButtonTemplate �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	        �� 5   
   " 	�6   .R�5      � 
������ .+�5      �� $
��back��6<   ���#EEE$ 5��6+   ._�5      �� #��z��6    $
��15,5q�6A   $��Center=�5       $��Center=�6;   5      5       5      P� 5       �@d�	IsChecked$
 TrueY�6/   . �5      �� $
 back��6C    � �� �#F7CCDF�� 5       � ��
 �����63   5      5      5      .��5      �� $ 5��6   
�������._�5   
   � 	 � RowDefinitions	 5      �� 5      $
��auto �6    �� 5      .+�5       �� ���#DDD$��0,0,0,1q�6(   $
��20,0q�6B   ._�5!      � $��10,18q�6   	 5"      �� 5#      $��35��6(   $
��auto �67   �� 5$      $��35��6(   $
��auto �67   �� 5%      $��35��6(   $
��auto �67   .+�5(      �� 
 �� Orientation$
 
Horizontal=�6!   $��Center=�6:   .+�5)      �� $��内容分类��6$   $��0,0,10,0q�60   $��Center=�6B   ���#6666]   �� 5*      ��6'   �� *CategoryCourses+��5+      �� 5,   "   .R�5-   &   �� $
 
Horizontal=�61   5.   #   ��50      �� 51   "   .R�52   &   	� #��[  6t   $	��5,0q�6�    	� 	GroupName$ course��53   2   ��52   2   �� *CategoryName+ d� 	IsChecked 6S   �� *
IsSelected+53   2   54   #   56      57      �� 58      $
 
Horizontal=�6!   $��1��6:   $��Center=�6G   .+�59      �� $��技术分类��6$   $��0,0,10,0q�61   $��Center=�6C   ���#6666^   �� 5:      ��6'   �� *CategoryTechnology+��5;      �� 5<   "   .R�5=   &   �� $
 
Horizontal=�61   5>   #   ��5@      �� 5A   "   .R�5B   &   	� #��[  6t   $	��5,0q�6�   $ 
technology��5C   2   ��5B   2   �� *CategoryName+ 6S   �� *
IsSelected+5C   2   5D   #   5F      5G      �� 5H      $
 
Horizontal=�6!   $��2��6:   $��Center=�6G   .+�5I      �� $��负责人员��6$   $��0,0,10,0q�61   $��Center=�6C   ���#6666^   �� 5J      ��6'   �� *CategoryCharge+��5K      �� 5L   "   .R�5M   &   �� $
 
Horizontal=�61   5N   #   ��5P      �� 5Q   "   .R�5V   &   	� -   5        #��[  5V   t   $	��5,0q�6�   $ Charge��5W   2   ��5V   2   �� *CategoryName+ 6S   �� *
IsSelected+5X   2   5Y   #   5[      5\      �� 5^      $��2��6   $��+ 添加课程��6*   $	��100��6;   $��30��6G   $��Right=�6S   $��Center=�6o   5_      5`      �� 5a      $��1��6   $��Hidden=�6(   .��5b      �� -   5        $��	aaCourses��5b   B   ��6   �� *
CourseList+��5c      �� 5d      .R�5e      $� 5f      ��5h      �� 5i      .R�5j      � .+�5k   "   � $��courseContent��6'    � ColumnDefinitions 5l   &   �� 5m   *   $	��150 �6;   �� 5n   *   �� 5o   *   $	��100 �6;   .+�5q   &   �� $��90��6-   $��60��68   $ 5��6D   $
��0,15q�6U    J� Effect 5r   *   3  -System.Windows.Media.Effects.DropShadowEffect  5s   .      Color$
 Gray��6?      ShadowDepth$ 0Q�6L      Opacity$	 0.3Q�6\      
BlurRadius$ 10Q�6j      	Direction$ 0Q�6z   ��5u   *   �� 5v   .    j� Stretch$ UniformToFill=�6W    �� ImageSource 69   �� *Cover+6W   5x   '   �� 5y   &   $��1��61   $��Center=�6A   .+�5z   *   �� $
 
Horizontal=�65   $��0,0,0,10q�6N   .+�5{   .   �� $��16(�68   �� 5|   &   ���#333 �� Command 6B   �� *DataContext.OpenUrl+ �� RelativeSource ��  �� AncestorType$ UserControlL� �� Mode$ FindAncestor=� �� CommandParameter 5~   0   �� *Url+.�6Q   ��5   *   �� 5�   .   .�5�   2   ��  � �� 6^   Z�  �� Triggers 5�   2   P� 5�   6    �@��IsMouseOver . . �5�   :   ��  � �� 5�   >   |� 5�   B   }� 5�   F    }� Location$ 	Underline=�6U   5�   C   5�   ;   5�   7   5�   /   .�5�   ;   .�5�   *   �� ��64   �� *
CourseName+5�   '   5�   '   �� 5�   .   $
��15,0q�6;   ��6I   �� *
Permanents+��5�   2   �� 5�   6   .R�5�   :   $�  $� Orientation$ 
Horizontal=�6Q   5�   7   ��5�   2   �� 5�   6   .R�5�   :   �� ���#EEE$	��5,0q�6S   $ 5��6`   ._�5�   >   �� $
��12,3q�6Y   ��6H   �� 6Y   5�   ;   5�   7   5�   /   5�   +   �� 5�   *   $
��Wrap=�6Q    �� 
LineHeight$ 20��6e   ���#AAA��64   �� *Description+6u   5�   '   �� 5�   &   $��2��61   $
 
Horizontal=�6A   $��Center=�6Z   $��Center=�6w   .+�5�   *   �� $	��5,0q�64   �� 5�   &   .�60   详情64   5�   +   �� 5�   *   $	��5,0q�64   �� 5�   &   .�60   删除64   5�   +   5�   '   5�   #   "  MyControllers.SkeletonScreen  5�   "   $��skeleton��6<   5�       �� Triggers 5�      �� 5�   "     �� Value$
  True��6Q   ! �� Binding! 6.   �� *IsShowSkeleton+.a�5�   &   �� $ skeleton��6S    � y�$ Visible=�6C   6S   �� 5�   &   $ courseContent��6U    � y�$ 	Collapsed=�6C   6U   5�   #   �� 5�   "   $  False��6Q   ! 6.   �� *IsShowSkeleton+.a�5�   &   �� $ skeleton��6U    � y�$ 	Collapsed=�6C   6U   �� 5�   &   $ courseContent��6S    � y�$ Visible=�6C   6S   5�   #   5�      5�      5�      5�      5�      5�      