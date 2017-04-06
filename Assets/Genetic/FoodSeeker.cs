using UnityEngine;

public class FoodSeeker : GenomeOwner
{
    private float currentFoodLikeliness;        //How much this guy likes food
    [SerializeField]
    private float maxFoodLikeliness;
    [SerializeField]
    private float minFoodLikeliness;
    [SerializeField]
    private float varFoodLikeliness;

    private float detectionRadius;
    [SerializeField]
    private float minDetectionRadius;
    [SerializeField]
    private float maxDetectionRadius;
    [SerializeField]
    private float varDetectionRadius;

    private float movementSpeed;

    private int foodEaten;
    
    private Vector3 randomPosition;
    private World world;

    protected override void Awake()
    {
        base.Awake();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        randomPosition = getRandomPositionInWorld();
        foodEaten = 0;
    }

    protected override void initializeGenome()
    {
        Genome = new GenomeBux();

        currentFoodLikeliness = Random.Range(minFoodLikeliness, maxFoodLikeliness);
        detectionRadius = Random.Range(minDetectionRadius, maxDetectionRadius);
        movementSpeed = 400;

        Genome.Genes.Add(new Gene(currentFoodLikeliness, minFoodLikeliness, maxFoodLikeliness, varFoodLikeliness));
        Genome.Genes.Add(new Gene(currentFoodLikeliness, minDetectionRadius, maxDetectionRadius, varDetectionRadius));

        Genome.Mutator = new MutatorRandom();
    }

    protected override float calculateFitness()
    {
        return foodEaten;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            collision.gameObject.GetComponent<Food>().eat();
            foodEaten++;
        }
    }

    private Vector3 getRandomPositionInWorld()
    {
        return world.getRandomPositionInWorld();
    }

    void Update()
    {
        bool hasInterestInFood = Random.Range(0.0f, 1.0f) < currentFoodLikeliness;
        int layerMask = 1 << 8;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);

        if (hasInterestInFood && colliders.Length > 0)
        {
            GameObject closestFood = colliders[0].gameObject;
            float distanceToClosestFood = 999999;
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == "Food")
                {
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    if (distanceToClosestFood > distance)
                    {
                        distanceToClosestFood = distance;
                        closestFood = collider.gameObject;
                    }
                }
            }

            transform.rotation = Quaternion.LookRotation(closestFood.transform.position - transform.position);
            //transform.LookAt(new Vector3(closestFood.transform.position.x, transform.position.y, closestFood.transform.position.z));
        }
        else
        {
            if (Vector3.Distance(transform.position, randomPosition) < 0.5f)
                randomPosition = getRandomPositionInWorld();
            transform.rotation = Quaternion.LookRotation(randomPosition - transform.position);
        }
        GetComponent<Rigidbody>().velocity = transform.forward * movementSpeed * Time.deltaTime;
    }
}
