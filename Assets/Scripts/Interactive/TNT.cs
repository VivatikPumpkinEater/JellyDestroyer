using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : InteractiveProps
{
    [SerializeField] private ParticleSystem _explosion = null;
    
    protected override void Slam()
    {
        var slamPunch = Physics2D.OverlapCircleAll(transform.position, 3f);

        for (int i = 0; i < slamPunch.Length; i++)
        {
            //var rb = inRadius[i].attachedRigidbody;
            var rb = slamPunch[i].attachedRigidbody;

            if (rb)
            {
                if (rb.position.x > transform.position.x)
                {
                    rb.AddForce((Vector2.up * 2 + Vector2.right) * _Force, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce((Vector2.up * 2 + Vector2.left) * _Force, ForceMode2D.Impulse);
                }

                var interactiveProps = rb.GetComponent<InteractiveProps>();
                
                if (interactiveProps)
                {
                    interactiveProps.Hit();
                }

                var enemy = rb.GetComponent<EnemyBase>();

                if (enemy)
                {
                    enemy.GetComponent<HealthManager>().Damage(1, transform.position);
                }
            }
        }
    }

    public override void Hit()
    {
        base.Hit();
    }

    public void Explosion()
    {
        Slam();
        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(gameObject);
    }
}
