/*
 * Tilemapʹ��˵��
 * 1.����һ��Scene
 * 2.��PlayerSet�������� ɾ��ԭ�е�MainCamera
 * 3.����һ��2D Object -> Tilemap -> Rectangular
 * 4.��ᷢ�ֳ�����Grid����Grid���洴���������� һ��BackGround һ��ForeGround
 * 5.��BackGroundֱ�����Tilemap����Լ�Tilemap Renderer��� 
 *      ��Tilemap Renderer�е�Sorting Layer��ΪBackground
 * 6.������grid�д���Tilemap ���������ForeGround���� ��������Tilemap��Sorting Layer����ForeGround
 * (Shadow����ΪShadow Ӱ�첻��
 * 7.�������Tilemap�ǳ�����ײ����Ŀ��� ��������Ҫ��Ground��Platform��Shadow���Ϸ��޸Ķ�Ӧ��Layer
 * 
 * 8.Ground�����ǲ������ֵ����ǽ�� ��Ϊ������ǽ ����ͳһΪGround ����Ҫ��Ground���Tilemap Collider 2D ��ѡUsed By Composite
 *  �����һ��Composite Collider 2D������Rigidbody 2D����ΪStatic����
 * 
 * 9.Platform������Ҫ�Ȼ������ƽ̨ Ȼ���м���ƽ̨��Ӽ���Box Collider 2D�����������������ƽ̨��С��
 * Ϊÿһ��Collider��ѡUsed By Effector Ȼ�����Platform Effector�����������Collider Mask��ΪPlayerCol������ֻ��PlayerCol��
 * 
 * 10.Shadow����StealthArea ��������Ҫ�����Tilemap����ΪStealthArea�� Ȼ����ͬƽ̨һ�� Ϊ��Ӱ���Polygon Collider Ϊÿһ��Collider��ѡIs Trigger����
 * 
 * 11.������Blaze�޷�������ʾ �����Blaze��Sprite Renderer��Sorting Layer�Ƿ����ΪForeGround �Լ����Tilemap��Sorting Layer�Ƿ���ȷ��
 * 
 * 12.��ؿ���죡
 */