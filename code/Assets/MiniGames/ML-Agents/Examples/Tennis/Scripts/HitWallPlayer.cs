using UnityEngine;
using UnityEngine.SceneManagement;

public class HitWallPlayer : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;

    private TennisArea m_Area;
    private TennisAgent m_AgentA;
    private TennisAgent m_AgentB;

    public GameObject levelComplete;
    public GameObject levelFailed;

    // Use this for initialization
    void Start()
    {
        levelFailed.gameObject.SetActive(false);
        levelComplete.gameObject.SetActive(false);
        m_Area = areaObject.GetComponent<TennisArea>();
        m_AgentA = m_Area.agentA.GetComponent<TennisAgent>();
        m_AgentB = m_Area.agentB.GetComponent<TennisAgent>();
    }



    private void Update()
    {
       if( m_AgentA.score >= 5)
        {
            if (SceneStats.agentOption == true)
            {
                SceneManager.LoadScene(2);
            }
            else {
                levelFailed.gameObject.SetActive(true);
            }
        }

        if (m_AgentB.score >= 5)
        {
            if (SceneStats.agentOption == true)
            {
                SceneManager.LoadScene(7);
            }
            else {
                levelComplete.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "over")
        {
            if (lastAgentHit == 0)
            {
                m_AgentA.AddReward(0.1f);
            }
            else
            {
                m_AgentB.AddReward(0.1f);
            }
            lastAgentHit = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("iWall"))
        {
            if (collision.gameObject.name == "wallA")
            {
                if (lastAgentHit == 1)
                {
                    m_AgentA.AddReward(-0.01f);
                    m_AgentB.SetReward(0);
                    m_AgentB.AddReward(+0.005f);

                    m_AgentB.score += 1;
                }
                else if (lastAgentHit ==0)
                {
                    m_AgentA.SetReward(0);
                    m_AgentB.AddReward(-0.01f);
                    m_AgentA.AddReward(+0.005f);

                    m_AgentA.score += 1;
                }
            }
            else if (collision.gameObject.name == "wallB")
            {
                if (lastAgentHit == 0)
                {
                    m_AgentA.AddReward(-0.01f);
                    m_AgentB.SetReward(0);
                    m_AgentB.AddReward(+0.005f);
                    m_AgentB.score += 1;
                }
                else if(lastAgentHit==1)
                {
                    m_AgentA.SetReward(0);
                    m_AgentB.AddReward(-0.01f);
                    m_AgentA.AddReward(+0.005f);

                    m_AgentA.score += 1;
                }
            }
            else if (collision.gameObject.name == "floorA")
            {
                if (lastAgentHit == 0 || lastAgentHit == -1)
                {
                    m_AgentA.AddReward(-0.01f);
                    m_AgentB.SetReward(0);
                    m_AgentB.AddReward(+0.005f);
                    m_AgentB.score += 1;
                }
                else
                {
                    m_AgentA.AddReward(-0.01f);
                    m_AgentB.SetReward(0);
                    m_AgentB.AddReward(+0.005f);
                    m_AgentB.score += 1;
                }
            }
            else if (collision.gameObject.name == "floorB")
            {
                if (lastAgentHit == 1 || lastAgentHit == -1)
                {
                    m_AgentA.SetReward(0);
                    m_AgentB.AddReward(-0.01f);
                    m_AgentA.AddReward(+0.005f);
                    m_AgentA.score += 1;
                }
                else
                {
                    m_AgentA.SetReward(0);
                    m_AgentB.AddReward(-0.01f);
                    m_AgentA.AddReward(+0.005f);
                    m_AgentA.score += 1;
                }
            }
            else if (collision.gameObject.name == "net")
            {
                if (lastAgentHit == 0)
                {
                    m_AgentA.AddReward(-0.01f);
                    m_AgentB.SetReward(0);
                    m_AgentB.AddReward(+0.005f);
                    m_AgentB.score += 1;

                }
                else
                {
                    m_AgentA.SetReward(0);
                    m_AgentB.AddReward(-0.01f);
                    m_AgentA.AddReward(+0.005f);
                    m_AgentA.score += 1;
                }
            }
            m_AgentA.Done();
            m_AgentB.Done();
            m_Area.MatchReset();
        }

        if (collision.gameObject.CompareTag("agent"))
        {
            lastAgentHit = collision.gameObject.name == "AgentA" ? 0 : 1;
        }
    }
}
