namespace Breifico.Functional

module LinkedList =
    type 'a LinkedListNode = FilledNode of 'a * LinkedListNode<'a> | NilNode

    let EmptyList = LinkedListNode.NilNode

    let rec add item = function
        | FilledNode(a, node)-> node :: add item node
        | NilNode -> FilledNode(item, NilNode)