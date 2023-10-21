using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour
{

    // �b�s�边���Ω�վ㪺���}�ܶq
    public Transform Ball;                        // ���x�y���Ѧ�
    public Transform PosDribble;                  // ���a�۹��m���B�y��m
    public Transform PosOverHead;                 // ���Y�ɪ��y��m�]���_�y�ɡ^
    public Transform Arms;                        // �缾�a���u���Ѧ�
    public Transform Target;                      // ���x���ؼ�

    // �p�����A�ܶq
    private bool IsBallInHands = true;            // �y�O�_�ثe�b�⤤
    private bool IsBallFlying = false;            // �y�O�_�b�b�Ť��]���b���Y�^
    private bool IsDribbling = false;             // ���a�O�_���b�B�y
    private float T = 0;                          // �Ω���Y�y���ɶ��p�ƾ�
    private Vector3 initialBallPosition;          // �y����l��m�]�Ω󭫸m�^

    void Start()
    {
        initialBallPosition = Ball.position;     // �x�s�y����l��m
    }

    // �C�@�V���|�Q�ե�
    void Update()
    {

        // �p�G�y�b�⤤
        if (IsBallInHands)
        {

            // ���Y���y�]�ǳƧ��Y�^
            if (Input.GetKey(KeyCode.Space))
            {
                Ball.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 360;
                // transform.LookAt(Target.parent.position);  // ���a�¦V�ؼ�
            }

            // �B�y�ʧ@
            else if (Input.GetKey(KeyCode.R))
            {
                IsDribbling = true;
                // �ϲy�W�U�_�ʥH�����B�y
                Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                Arms.localEulerAngles = Vector3.right * 0;
            }

            // ����B�y�ʧ@
            else if (Input.GetKeyUp(KeyCode.R))
            {
                IsDribbling = false;
            }

            // ���Y�y
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
            }
        }

        // �y�b�Ť��������޿�]�Q��X��^
        if (IsBallFlying)
        {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // ��y�����ʶi��u�ʴ��ȡA�Ϩ�¦V�ؼ�
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // ���y�����Y�W�[�ߪ��u�B��
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            // �y��F�ؼЮɪ��޿�
            if (t01 >= 1)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(ResetBallPosition());  // 2��᭫�m�y����m
            }
        }
    }

    // �ˬd���a�O�_�a��y�H�B�_��
    private void OnTriggerEnter(Collider other)
    {
        if (!IsBallInHands && !IsBallFlying)
        {
            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // ��{�ӭ��m�y����m
    IEnumerator ResetBallPosition()
    {
        yield return new WaitForSeconds(2);   // ����2��
        Ball.position = initialBallPosition; // �N�y���m���l��m
        Ball.GetComponent<Rigidbody>().isKinematic = true; // �ϲy�ܬ��B�ʾǪ��A�o�˥����|�]���z��]�ӱ��U
        IsBallInHands = true;
    }
}