using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class Cat : MonoBehaviour, IDamageable
{
    protected string ID;    //캐릭터 넘버
    protected int Lv = 0;  //레벨
    protected double hp = -999;  //체력   valueType? : null값이 들어가 있으면
    protected double maxHp = -999;  //최대체력
    protected double attack;  //공격력
    //protected double? protect = null; //방어력
    //protected double? healing = null; //회복력
    protected float criticalHit = 1; //치명타율
    protected float criticalDamage = 0; //치명타데미지 
    protected float bossAttack = 0;  //보스 데미지 피해량 
    protected bool passiveAct = false; //패시브 활성화 유무
    protected bool dead = false;    //죽음확인

    //장비 멀로 처리하냐

protected virtual void hpInit(){    //체력 초기화
        if(hp == -999 || maxHp == -999){
            Debug.Log("ID");
            Debug.Log("hp error!");
        }
        else
        {
            hp = maxHp;
            dead = false;
        }
        //체력이 0보다 작을 경우 초기화가 실행 되어야함
    }

    //캐릭터 데미지를 입히고 싶을 때 레이캐스트 판정(tag) Cat걸리면 실행시키면 됨 당사자의 데미지를 까고 0이면 hit 캐릭터 삭제?
    public virtual void OnDamage(double Damage, RaycastHit hit) //캐릭터 데미지 입히는 호출
    {
        //데미지 입음
        double damageAdd = Damage;//영웅 방어력*방어력(보유효과)*성급효과(뭔지모르겠음)*패시브스킬(유무)
        hp -= damageAdd;
        if(hp <= 0 && !dead!) { Die(); }    //죽음처리
        //패시브 스킬이 있는경우 오버라이딩으로 작업
    }

    /*protected virtual void healingApply(){
        //체력 회복
        if(healing == null){
            Debug.Log("ID");
            Debug.Log("healing error!");
            return;
        }
        //영웅 회복력*회복력(보유효과)*장비장착효과(유무)*성급효과*별자리(유무)
    }*/

    public virtual double attackApply(){
        //일반 공격값 반환
        if(attack == 0){
            Debug.Log("ID");
            Debug.Log("attack error!");
            return 0;
        }
        double AllAttack = attack;
        //영웅 공격력*공격력(보유효과)*성급효과*장비장착효과*패시브스킬*별자리
        //패시브 스킬은 어떻게 짤건지 고민 + 크리티컬 데미지 작업도 필요함
        return AllAttack;
    }

    public virtual double bossAttackApply(){
        //보스전용 공격값 반환
        double AllAttack = attack+bossAttack;
        //영웅 (공격력*공격력(보유효과)*성급효과*장비장착효과*패시브스킬*별자리) + 보스피해량
        //패시브 스킬이 있는경우 오버라이딩으로 작업
        return AllAttack;
    }
    private void Die()
    {
        dead = true;
        //캐릭터 죽는 모션 리셋 쿨타임 후 inithp
        //코루틴 써야할 듯
    }

    private void UpdateData()
    {
        //작성
    }

    public void LevelUP()
    {
        Lv++;
        //데이터 전달 후 값 업데이트
    }

    protected void printData()
    {
        Debug.Log(ID+" hp: "+Unit.ToUnitString(hp));
        Debug.Log(ID + "maxHp: " +Unit.ToUnitString(maxHp));
        Debug.Log(ID + "attack: " +Unit.ToUnitString(attack));
        Debug.Log(ID + "Lv: " +Lv);
    }
}
