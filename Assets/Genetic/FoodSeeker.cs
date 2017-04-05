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

    protected override void initializeGenome()
    {
        Genome = new GenomeBux();

        currentFoodLikeliness = Random.Range(minFoodLikeliness, maxFoodLikeliness);
        detectionRadius = Random.Range(minDetectionRadius, maxDetectionRadius);
        movementSpeed = 100;

        Genome.Genes.Add(new Gene(currentFoodLikeliness, minFoodLikeliness, maxFoodLikeliness, varFoodLikeliness));
        Genome.Genes.Add(new Gene(currentFoodLikeliness, minDetectionRadius, maxDetectionRadius, varDetectionRadius));

        Genome.Mutator = new MutatorRandom();
    }

    protected override float calculateFitness()
    {
        float fitness = 0;
        fitness += (0.3f - (Genome.Genes[0].Value) * 0.3f);
        fitness += (0.3f - (Genome.Genes[1].Value) * 0.3f);
        fitness += (0.3f - (Genome.Genes[2].Value) * 0.3f);
        return fitness;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            collision.gameObject.GetComponent<Food>().eat();

        }
    }

    void Update()
    {
        float randomRotation = 0;
        bool hasInterestInFood = Random.Range(0.0f, 1.0f) < currentFoodLikeliness;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

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

            transform.LookAt(new Vector3(closestFood.transform.position.x, transform.position.y, closestFood.transform.position.z));
        }
        else
        {
            randomRotation = Random.Range(0.0f, 0.1f);
        }
        transform.Rotate(transform.rotation.x, transform.rotation.y + randomRotation, transform.rotation.z);
        GetComponent<Rigidbody>().velocity = transform.forward * movementSpeed * Time.deltaTime;
        Debug.Log(transform.forward);
    }
}
